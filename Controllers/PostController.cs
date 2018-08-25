using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlogApi.Models;

namespace BlogApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PostController : ControllerBase
  {
    private readonly PostContext _context;

    public PostController(PostContext context)
    {
      _context = context;
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
    public IActionResult Create(Post post)
    {
      post.DateCreated = new System.DateTime();
      _context.Posts.Add(post);
      _context.SaveChanges();

      return CreatedAtRoute("GetPost", new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, Post post)
    {
      var updatedPost = _context.Posts.Find(id);
      if (post == null)
      {
        return NotFound();
      }

      updatedPost.Title = post.Title;
      updatedPost.Body = post.Body;

      _context.Posts.Update(updatedPost);
      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var todo = _context.Posts.Find(id);
      if (todo == null)
      {
        return NotFound();
      }

      _context.Posts.Remove(todo);
      _context.SaveChanges();
      return NoContent();
    }
  }
}
