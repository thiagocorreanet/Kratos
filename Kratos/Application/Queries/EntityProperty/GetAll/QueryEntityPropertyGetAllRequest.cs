using MediatR;

namespace Application.Queries.EntityProperty.GetAll;

public class QueryEntityPropertyGetAllRequest : IRequest<IEnumerable<QueryEntityPropertyGetAllResponse>>
{
    public List<QueryEntityPropertyGetAllResponse> ToResponse(List<Core.Entities.EntityProperty> entities)
    {
        List<QueryEntityPropertyGetAllResponse> queryEntityPropertyGetAllResponse = new List<QueryEntityPropertyGetAllResponse>();

        foreach (var entity in entities)
        {
            queryEntityPropertyGetAllResponse.Add(new QueryEntityPropertyGetAllResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type, 
                IsRequired = entity.IsRequired,
                EntityId = entity.EntityId,
            });
        }

        return queryEntityPropertyGetAllResponse;
    }
}

