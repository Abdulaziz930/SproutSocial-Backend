using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SproutSocial.Application.Abstractions.Storage;
using SproutSocial.Application.Abstractions.Storage.Local;

namespace SproutSocial.Infrastructure.Services.Storage.Local;

public class LocalStorage : Storage, ILocalStorage
{
    private readonly IWebHostEnvironment _environment;

    public LocalStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public void Delete(string path, string fileName)
    {
        if (File.Exists(Path.Combine(_environment.WebRootPath, path, fileName)))
            File.Delete(Path.Combine(_environment.WebRootPath, path, fileName));
    }

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
        => File.Exists(Path.Combine(path, fileName));

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_environment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            string newFileName = FileRename(uploadPath, file.FileName, HasFile);

            await CopyFileAsync(Path.Combine(uploadPath, newFileName), file);
            datas.Add((newFileName, Path.Combine(path, newFileName)));
        }

        return datas;
    }

    public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string path, IFormFile file)
    {
        string uploadPath = Path.Combine(_environment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        string newFileName = FileRename(uploadPath, file.FileName, HasFile);

        await CopyFileAsync(Path.Combine(uploadPath, newFileName), file);

        return (newFileName, Path.Combine(path, newFileName));
    }

    async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            return true;
        }
        catch (Exception ex)
        {
            //TODO: Loging
            throw ex;
        }
    }
}
