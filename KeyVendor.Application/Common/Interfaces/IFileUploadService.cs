using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Application.Common.Interfaces;

public interface IFileUploadService
{
    Task SaveKeyImage(IFormFile file, string fileName);
    FileStream GetImage(string id);
}