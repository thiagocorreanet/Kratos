using Application.Queries.Project.GetAll;
using Application.Queries.Project.GetById;
using MediatR;

namespace Application.Queries.Entitie.GetById;

public class QueryEntityGetByIdRequest : IRequest<QueryEntityGetByIdResponse>
{
    public QueryEntityGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public QueryEntityGetByIdResponse ToResponse(Core.Entities.Entity entity, List<Core.Entities.Project> listProjects)
    {

        QueryEntityGetByIdResponse response = new QueryEntityGetByIdResponse();

        response.Id = Id;
        response.Name = entity.Name;
        response.CreatedAtShortDate = entity.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
        response.AlteredAtShortDate = entity.AlteredAt.ToString("dd/MM/yyyy HH:mm:ss");
        response.ProjectId = entity.ProjectId;
        response.ProjectRel = listProjects.Select(x => new QueryProjectGetAllResponse
        {
            Id= x.Id,
            Name= x.Name,
        }).ToList();

        return response;

    }
}
