using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;
using BlogApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace BlogApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PostController : ControllerBase
  {
    // TODO - factor out DB logic as per: https://stackoverflow.com/questions/35743811/how-can-i-test-asp-net-mvc-post-controller-with-moq-and-nunit
    public static IConfiguration Configuration { get; set; }
    public string AWS_KEY { get; private set; }
    public string AWS_SECRET { get; private set; }
    private readonly PostContext _context;
    public AmazonUploader uploader { get; set; }

    public PostController(PostContext context, IConfiguration config)
    {
      _context = context;
      Configuration = config;
      AWS_KEY = Configuration["AWS_KEY"];
      AWS_SECRET = Configuration["AWS_SECRET"];
      uploader = new AmazonUploader(AWS_KEY, AWS_SECRET);
    }

    [HttpGet]
    public ActionResult<List<Post>> GetAll()
    {
      return _context.Posts.ToList();
    }

    [HttpGet("{id}", Name = "GetPost")]
    public ActionResult<Post> GetById(long id)
    {
      var post = _context.Posts.Find(id);
      if (post == null)
      {
        return NotFound();
      }
      return post;
    }

    [HttpPost]
    public CreatedAtRouteResult Create(Post post)
    {
      post.DateCreated = new System.DateTime();
      _context.Posts.Add(post);
      _context.SaveChanges();

      return CreatedAtRoute("GetPost", new { id = post.Id }, post);
    }

    [HttpPost("{id}")]
    public IActionResult Update(long id, Post post)
    {
      var updatedPost = _context.Posts.Find(id);
      if (post == null)
      {
        return NotFound();
      }

      updatedPost.Title = post.Title;
      updatedPost.Body = post.Body;
      updatedPost.ImageId = post.ImageId;

      _context.Posts.Update(updatedPost);
      _context.SaveChanges();
      return Ok(_context.Posts);
    }

    // TODO refactor and rename
    [HttpPost("image/{id:long}")]
    public async Task<IActionResult> Image(long id)
    {
      var posts = _context.Posts;
      var updatedPost = posts.Find(id);
      if (updatedPost == null)
      {
        return NotFound();
      }
      var request = HttpContext.Request;
      var fileStream = request.Body;
      var contentType = request.ContentType;
      var contentLength = request.ContentLength;
      var key = Guid.NewGuid().ToString();

      var length = contentLength.HasValue ? (long)contentLength : 0;
      var result = await uploader.sendMyFileToS3(fileStream, contentType, length, key);

      if (result == "error")
        return InternalServerError();
      updatedPost.ImageId = result;
      _context.Posts.Update(updatedPost);
      _context.SaveChanges();
      return Ok(result);
    }

    private IActionResult InternalServerError()
    {
      throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var post = _context.Posts.Find(id);
      if (post == null)
      {
        return NotFound();
      }

      _context.Posts.Remove(post);
      _context.SaveChanges();
      return NoContent();
    }
  }
}
