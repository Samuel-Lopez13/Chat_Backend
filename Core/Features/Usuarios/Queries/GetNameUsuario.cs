using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Queries;

public record GetNameUsuario : IRequest<GetNameUsuarioResponse>;

public class GetNameUsuarioHandler : IRequestHandler<GetNameUsuario, GetNameUsuarioResponse>
{
    private readonly ChatContext _context;
    private readonly IAuthorization _authorization;
    
    public GetNameUsuarioHandler(ChatContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<GetNameUsuarioResponse> Handle(GetNameUsuario request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id_Usuario == _authorization.UsuarioActual());

        var response = new GetNameUsuarioResponse()
        {
            Nombre = usuario.UserName
        };

        return response;
    }
}

public record GetNameUsuarioResponse
{
    public string Nombre { get; set; }
}