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

    public QueryEntityGetByIdResponse ToResponse(Core.Entities.Entity entity)
    {

        QueryEntityGetByIdResponse response = new QueryEntityGetByIdResponse();

        response.Id = Id;
        response.Name = entity.Name;
        response.CreatedAtShortDate = entity.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
        response.AlteredAtShortDate = entity.AlteredAt.ToString("dd/MM/yyyy HH:mm:ss");

        return response;

    }
}
