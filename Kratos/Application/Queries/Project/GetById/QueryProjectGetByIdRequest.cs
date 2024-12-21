using MediatR;

namespace Application.Queries.Project.GetById;

public class QueryProjectGetByIdRequest : IRequest<QueryProjectGetByIdResponse>
{

    public QueryProjectGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public QueryProjectGetByIdResponse ToResponse(Core.Entities.Project entity)
    {

        QueryProjectGetByIdResponse response = new QueryProjectGetByIdResponse();

        response.Id = Id;
        response.Name = entity.Name;
        response.CreatedAtShortDate = entity.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
        response.AlteredAtShortDate = entity.AlteredAt.ToString("dd/MM/yyyy HH:mm:ss");

        return response;

    }
}
