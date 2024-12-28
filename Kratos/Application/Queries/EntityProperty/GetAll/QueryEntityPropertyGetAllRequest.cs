using System.Xml.Linq;
using Application.Queries.TypeData.GetAll;
using MediatR;

namespace Application.Queries.EntityProperty.GetAll;

public class QueryEntityPropertyGetAllRequest : IRequest<IEnumerable<QueryEntityPropertyGetAllResponse>>
{
    public List<QueryEntityPropertyGetAllResponse> ToResponse(List<Core.Entities.EntityProperty> entities)
    {
        return entities.Select(e => new QueryEntityPropertyGetAllResponse
        {
            Id = e.Id,
            Name = e.Name,
            TypeDataId = e.TypeDataId,
            IsRequired = e.IsRequired,
            EntityId = e.EntityId,
            TypeDataDescription = e.TypeDataRel?.Name ?? string.Empty,
        }).ToList();
    }
}