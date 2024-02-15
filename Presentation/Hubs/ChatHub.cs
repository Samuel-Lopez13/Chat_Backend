using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class ChatHub : Hub
{
    public async Task Send(string message)
    {
        // Lógica para enviar el mensaje a todos los clientes conectados
        await Clients.All.SendAsync("sendMessage", message);
    }
}