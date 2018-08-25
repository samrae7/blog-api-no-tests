using BlogApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace BlogApi
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", 
                     optional: false, 
                     reloadOnChange: true)
        .AddEnvironmentVariables();

      Configuration = builder.Build();
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<PostContext>(opt =>
          opt.UseSqlite(Configuration.GetConnectionString("PostContext")));
      services.AddMvc()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }
    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(builder =>
        builder.WithOrigins("http://localhost:8000")
           .AllowAnyHeader()
           .AllowAnyMethod()
        );
      app.UseMvc();
    }
  }
}