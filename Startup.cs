﻿using BlogApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Swagger;

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

      services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new Info { Title = "Sams Blog API", Version = "v1" });
      });
    }
    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(builder =>
        builder.WithOrigins("http://localhost:8000")
           .AllowAnyHeader()
           .AllowAnyMethod()
        );

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sams Blog API V1");
      });
      app.UseMvc();
    }
  }
}