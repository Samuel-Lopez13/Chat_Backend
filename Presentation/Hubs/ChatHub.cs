using Core.Domain.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.Modelos;

namespace Presentation.Hubs;

//[EnableCors("AllowOrigin")]
public class ChatHub : Hub
{
    private readonly IConvertToBase64 _base64;

    public override Task OnConnectedAsync()
    {
        // Configura CORS aquí si es necesario
        Context.GetHttpContext().Response.Headers.Add("Access-Control-Allow-Origin", "https://chatearapp.netlify.app");
        return base.OnConnectedAsync();
    }
    
    public ChatHub(IConvertToBase64 base64)
    {
        _base64 = base64;
    }
    
    public async Task SendMessage(string room, string user, string message)
    {
        //byte[] messageBytes = System.Text.Encoding.Unicode.GetBytes(message);
        
        await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
    }
    
    public async Task SendSticker([FromForm] Sticker stckr)
    {
        Console.WriteLine(stckr.sticker);
        string serializacion = _base64.ConvertBase64(stckr.sticker);
        
        await Clients.Group(stckr.room).SendAsync("ReceiveSticker", stckr.user, serializacion);
    }

    public async Task AddToGroup(string room)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, room);
        
        await Clients.Group(room).SendAsync("ShowWho", $"Alguien se conecto {Context.ConnectionId}");
    }
}