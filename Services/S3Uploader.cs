using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;

namespace BlogApi.Services
{
  public class AmazonUploader
  {
    private IAmazonS3 client;
    private const string bucketName = "secret-london-blog"; 
    private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest2;

    public AmazonUploader(string AWS_KEY, string AWS_SECRET) {
      client = new AmazonS3Client(AWS_KEY, AWS_SECRET, bucketRegion);
    }

    public async Task<ListObjectsResponse> ListingObjectsAsync()
    {            
        ListObjectsRequest request = new ListObjectsRequest
        {
            BucketName = bucketName
        };
        return await client.ListObjectsAsync(request);
    }
    public async Task<PutObjectResponse> sendMyFileToS3(System.IO.Stream inputStream, string contentType, long contentLength, string key)
    {
      PutObjectRequest request = new PutObjectRequest
      {
        BucketName = bucketName,
        Key = key,
        ContentType = contentType,
        InputStream = inputStream 
      };
      request.Headers.ContentLength = contentLength;
      return await client.PutObjectAsync(request);
    }
  }
}