namespace Core.Domain.Services;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string email, string password);
}