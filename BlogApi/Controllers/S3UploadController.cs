using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApi;
using BlogApi.Models;
using BlogApi.Services;
using Amazon.S3.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BlogApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class S3UploadController : ControllerBase
  {
    public string AWS_KEY {get; private set;}
    public string AWS_SECRET {get; private set;}
    public static IConfiguration Configuration { get; set; }
    private AmazonUploader uploader { get; set; }

    public S3UploadController(IConfiguration config)
    {
      Configuration = config;
      AWS_KEY = Configuration["AwsKey"];
      AWS_SECRET = Configuration["AwsSecret"];
      uploader = new AmazonUploader(AWS_KEY, AWS_SECRET);
    }

    [HttpGet]
    public ListObjectsResponse GetAll()
    {
      return uploader.ListingObjectsAsync().Result;
    }
      
    [HttpPost]
    public string MyFileUpload()
    {
      var request = HttpContext.Request;
      var fileStream = request.Body;
      var contentLength = request.ContentLength;
      var filename = request.Headers["filename"];
      var contentType = request.ContentType;
      // TODO ensure key is unique
      string key = filename;

      var length = contentLength.HasValue ? (long)contentLength : 0;
      return uploader.sendMyFileToS3(fileStream, contentType, length, key).Result;
    }
  }
}
