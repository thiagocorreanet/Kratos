using System.Runtime.Serialization;
using MediatR;

namespace Application.Queries.TypeData.GetById;

public class QueryTypeDataGetByIdRequest : IRequest<QueryTypeDataGetByIdResponse>
{
    private const string DateFormat = "dd/MM/yyyy HH:mm:ss";
    
    public QueryTypeDataGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public QueryTypeDataGetByIdResponse ToResponse(Core.Entities.TypeData entity)
    {
        QueryTypeDataGetByIdResponse queryTypeDataGetByIdResponse = new QueryTypeDataGetByIdResponse();

        return new QueryTypeDataGetByIdResponse()
        {
            Id = Id,
            Name = entity.Name,
            CreatedAtShortDate = entity.CreatedAt.ToString(DateFormat),
            AlteredAtShortDate = entity.AlteredAt.ToString(DateFormat)
        };
    }
}