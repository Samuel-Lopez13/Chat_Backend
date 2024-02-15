using Core.Features.Usuarios.Command;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.Hubs;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Inicio de sesion
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Devuelve el token de acceso
    /// <br/><br/>
    /// <b>400:</b> Si el usuario no se encuentra registrado
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<LoginResponse> Login([FromBody] Login command)
    {
        return await _mediator.Send(command);
    }
    
    /*[HttpGet]
    public async Task<IActionResult> Send(string message, string groupName)
    {
        await _hubContext.Clients.Group(groupName).SendAsync("sendMessage", message);
        return Ok();
    }*/
}