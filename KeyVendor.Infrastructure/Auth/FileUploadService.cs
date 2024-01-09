using KeyVendor.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalCar.Infrastructure.Configuration;

namespace KeyVendor.Infrastructure.Auth;

public class FileUploadService : IFileUploadService
{
    private readonly UploadConfiguration _uploadConfiguration;

    public FileUploadService(IOptions<UploadConfiguration> uploadConfiguration)
    {
        _uploadConfiguration = uploadConfiguration.Value;
    }

    public async Task SaveKeyImage(IFormFile file, string fileName)
    {
        var filePath = Path.Combine(_uploadConfiguration.UploadPath!, fileName + ".jpeg");
        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }

    public FileStream GetImage(string id)
    {
        var path = Path.Combine(_uploadConfiguration.UploadPath!, id + ".jpeg");
        var image = System.IO.File.OpenRead(path);
        return image;
    }
}