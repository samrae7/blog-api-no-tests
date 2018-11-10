﻿using System;
using BlogApi.Models;
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
                     reloadOnChange: true);
        // .AddEnvironmentVariables();

      Configuration = builder.Build();
    }
    
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // Use SQL Database if in Azure, otherwise, use SQLite
      if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        services.AddDbContext<PostContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
      else
        services.AddDbContext<PostContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("PostContext")));

      // Automatically perform database migration
      services.BuildServiceProvider().GetService<PostContext>().Database.Migrate();

      services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new Info { Title = "Sams Blog API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(builder =>
        builder.WithOrigins("http://localhost:8000", "https://blogapi.azurewebsites.net")
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