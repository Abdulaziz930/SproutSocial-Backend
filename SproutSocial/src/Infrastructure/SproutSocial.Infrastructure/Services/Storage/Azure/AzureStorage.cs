using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SproutSocial.Application.Abstractions.Storage.Azure;

namespace SproutSocial.Infrastructure.Services.Storage.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _blobContainerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Storage:Azure"]);
    }

    public void Delete(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        blobClient.Delete();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }
    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string containerName)> datas = new();
        foreach (var file in files)
        {
            string newFileName = FileRename(containerName, file.FileName, HasFile);

            BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((newFileName, $"{containerName}/{newFileName}"));
        }

        return datas;
    }

    public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string containerName, IFormFile file)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string containerName)> datas = new();
        string newFileName = FileRename(containerName, file.FileName, HasFile);

        BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
        await blobClient.UploadAsync(file.OpenReadStream());

        return (newFileName, $"{containerName}/{newFileName}");
    }
}
