using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Grupos.Queries;

public record GetGroups : IRequest<List<GetGroupsResponse>>;

public class GetGroupsHandler : IRequestHandler<GetGroups, List<GetGroupsResponse>>
{
    private readonly ChatContext _context;
    
    public GetGroupsHandler(ChatContext context)
    {
        _context = context;
    }

    public async Task<List<GetGroupsResponse>> Handle(GetGroups request, CancellationToken cancellationToken)
    {
        var grupos = await _context.Grupos.ToListAsync();
        
        var gruposLista = grupos.Select(grupo => new GetGroupsResponse()
        {
            group_id = grupo.Id_Grupo,
            name = grupo.GroupName
        }).ToList();

        return gruposLista;
    }
}

public record GetGroupsResponse
{
    public int group_id { get; set; }
    public string name { get; set; }
}