using System.ComponentModel.DataAnnotations;
using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Command;

public class Login : IRequest<LoginResponse>
{
    /// <example>
    /// samuelopez
    /// </example>
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Contrasena { get; set; }
}

public class LoginCommandHandler : IRequestHandler<Login, LoginResponse>
{
    private readonly IAuthService _authService;
    private readonly ChatContext _context;
    
    public LoginCommandHandler(IAuthService authService, ChatContext context)
    {
        _authService = authService;
        _context = context;
    }
    
    public async Task<LoginResponse> Handle(Login request, CancellationToken cancellationToken)
    {
        //Valida que no esten vacias
        if(string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Contrasena))
            throw new BadRequestException("Usuario y contraseña son obligatorios");
        
        //Si cumple con las validaciones se procede a autenticar
        var token = await _authService.AuthenticateAsync(request.UserName, request.Contrasena);
        
        if(token == null)
            throw new NotFoundException("Usuario no encontrado");
        
        //Respuesta
        return new LoginResponse()
        {
            Token = token,
        };
    }
}

public record LoginResponse
{
    public string Token { get; set; }
    
}