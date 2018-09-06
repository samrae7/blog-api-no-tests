using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models
{
  public class PostContext : DbContext
  {
    public PostContext(DbContextOptions<PostContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Post>().HasData(
        new Post
          {
            Id = 1,
            Title = "William Shakespeare",
            Intro = "THis is an intro1",
            Body = "Much ado about nothing"
          },
        new Post
          {
            Id = 2,
            Title = "Jane Austen",
            Intro = "THis is an intro2",
            Body = "Pride and Prejudice"
          },
        new Post
          {
            Id = 3,
            Title = "Evelyn Waugh",
            Intro = "THis is an intro3",
            Body = "Decline anf Fall"
          },
        new Post
          {
            Id = 4,
            Title = "George Eliot",
            Intro = "THis is an intro4",
            Body = "Middlemarch"
          },
        new Post
          {
            Id = 5,
            Title = "Charles Dickens",
            Intro = "THis is an intro5",
            Body = "Great Expectations"
          }
      );
    }
  }
}