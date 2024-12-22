using MediatR;

namespace Application.Queries.EntityProperty.GetById;

public class QueryEntityPropertyGetByIdRequest : IRequest<QueryEntityPropertyGetByIdResponse>
{
    public QueryEntityPropertyGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public QueryEntityPropertyGetByIdResponse ToResponse(Core.Entities.EntityProperty entity)
    {
        return new QueryEntityPropertyGetByIdResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type,
            IsRequired = entity.IsRequired,
            EntityId = entity.EntityId,
        };
    }
}
