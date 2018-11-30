using BlogApi;
using BlogApi.Models;
using BlogApi.Controllers;
using BlogApi.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.tests
{
  // This file was auto-generated based on version 1.2.0 of the canonical data.
  public class PostControllerTests
  {
    PostController controller;
    List<Post> posts;
    Mock<DbSet<Post>> mockSet;
    Mock<PostContext> mockContext;

    public PostControllerTests()
    {
      posts = GetTestPosts();
      mockSet = GetQueryableMockDbSet<Post>(posts);
      var mockContextOptions = new DbContextOptions<PostContext>();
      mockContext = new Mock<PostContext>(mockContextOptions);
      mockContext.Setup(m => m.Posts).Returns(mockSet.Object);
      mockContext.Setup(m => m.SaveChanges());

      var mockConfig = new Mock<IConfiguration>();
      mockConfig.Setup(m => m["AWS_KEY"]).Returns("my-aws-key");
      mockConfig.Setup(m => m["AWS_SECRET"]).Returns("my-aws-secret");

      controller = new PostController(mockContext.Object, mockConfig.Object);
    }

    [Fact]
    public void GetAllPosts()
    {
      var result = controller.GetAll();
      Assert.Equal(posts, result.Value);
    }

    [Fact]
    public void createPost()
    {
      Post newPost = new Post {Title = "fooTitle"};
      var createdResult = controller.Create(newPost);
      // add tests for DB update.
      Assert.NotNull(createdResult);
      Assert.Equal("GetPost", createdResult.RouteName);
      Assert.Equal(newPost.Id, createdResult.RouteValues["Id"]);
    }

    [Fact]
    public async void UploadPostImage()
    // TODO refactor so that we are not reliant on sealed class (?) HttpContext
    {
      var controllerCtx = new ControllerContext();
      controllerCtx.HttpContext = new DefaultHttpContext();
      controller.ControllerContext = controllerCtx;

      var mockAmazonUploader = new Mock<AmazonUploader>("foo", "bar");
      var task = Task.FromResult("foo");
      mockAmazonUploader.Setup(mock => mock.sendMyFileToS3(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>())).Returns(task);
      // TODO inject this so we don't have to mock like this
      controller.uploader = mockAmazonUploader.Object;

      OkObjectResult result = (OkObjectResult) await controller.Image(1);

      // mockAmazonUploader.Verify(mock => mock.sendMyFileToS3(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>()), Times.Once());
      mockAmazonUploader.Verify();
      Assert.Equal(task.Result, result.Value);

      // TODO test that the correct post is updated with the return value from sendMyFilesToS3
      // TODO test that the method errors if sendMyFilesToS3 errors
      // TODO test that the method returns NotFound if no matching post
      // TODO separate tests out and rename test methods
    }

    [Fact]
    public void deletePost()
    {
      var result = (NoContentResult) controller.Delete(2);
      Assert.Equal(result.StatusCode, controller.NoContent().StatusCode);
      // rather than test this way wrap the db stuff in a class and test that the class method is called
      mockSet.Verify(m => m.Find(It.IsAny<long>()), Times.Once);
      mockSet.Verify(m => m.Remove(It.IsAny<Post>()), Times.Once);
      mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    private static Mock<DbSet<Post>> GetQueryableMockDbSet<T>(List<Post> sourceList) where T : class
    {
      var queryable = sourceList.AsQueryable();

      var dbSet = new Mock<DbSet<Post>>();
      dbSet.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(queryable.Provider);
      dbSet.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(queryable.Expression);
      dbSet.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
      dbSet.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
      dbSet.Setup(d => d.Add(It.IsAny<Post>())).Callback<Post>((s) => sourceList.Add(s));
      dbSet.Setup(d => d.Find(It.IsAny<long>())).Returns(sourceList[0]);
      dbSet.Setup(d => d.Remove(It.IsAny<Post>()));
      return dbSet;
    }

    private List<Post> GetTestPosts()
    {
      var posts = new List<Post>();
      posts.Add(new Post()
      {
        Id = 1,
        Title = "First test post",
        Intro = "bafafaf",
        Body = "afafaf",
        ImageId = "fasfaf",
        DateCreated = new System.DateTime()
      });
      posts.Add(new Post()
      {
        Id = 2,
        Title = "Second test post",
        Intro = "bafafaf",
        Body = "afafaf",
        ImageId = "fasfaf",
        DateCreated = new System.DateTime()
      });
      return posts;
    }
  }
}
