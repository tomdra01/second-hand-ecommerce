using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Services;

public class MinioStorageService : IFileStorageService
{
    private readonly IMinioClient _client;
    private readonly string _bucketName = "item-images";

    public MinioStorageService(string endpoint, string accessKey, string secretKey)
    {
        _client = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();
    }

    public async Task UploadFileAsync(string objectName, Stream stream, string contentType)
    {
        await EnsureBucketExists();

        await _client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(contentType));
    }

    public string GetFileUrl(string objectName)
    {
        return $"http://localhost:9000/{_bucketName}/{objectName}";
    }

    private async Task EnsureBucketExists()
    {
        var found = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!found)
        {
            await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }
    }
}