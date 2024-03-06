using Core.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Core.Features.Usuario.Command;

public class ConvertToBase64 : IConvertToBase64
{
    public string ConvertBase64(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();
        return Convert.ToBase64String(fileBytes);
    }
}