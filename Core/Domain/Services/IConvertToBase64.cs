using Microsoft.AspNetCore.Http;

namespace Core.Domain.Services;

public interface IConvertToBase64
{
    string ConvertBase64(IFormFile file);
}