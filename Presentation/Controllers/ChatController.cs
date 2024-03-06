using Core.Features.Grupos.Queries;
using Core.Features.Usuarios.Command;
using Core.Features.Usuarios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.Hubs;
using Presentation.Modelos;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IMediator mediator, IHubContext<ChatHub> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
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
    
    [HttpGet("UserName")]
    public async Task<GetNameUsuarioResponse> NombreUsuario()
    {
        return await _mediator.Send(new GetNameUsuario());
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<GetGroupsResponse>> GruposListas() 
    {
        return await _mediator.Send(new GetGroups());
    }
    
    [AllowAnonymous]
    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] MessageData messageData)
    {
        await _hubContext.Clients.Group(messageData.room).SendAsync("ReceiveMessage", messageData.user, messageData.message);
        return Ok();
    }
    
}