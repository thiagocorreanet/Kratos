using Application.Queries.EntityProperty.GetAll;
using MediatR;

namespace Application.Queries.GenerateCode.GetById;

public class QueryGenerateCodeGetByIdRequest : IRequest<QueryGenerateCodeGetByIdResponse>
{
    public QueryGenerateCodeGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public List<QueryEntityPropertyGetAllResponse> ToResponse(List<Core.Entities.EntityProperty> properties)
    {
        return properties
            .Select(c => new QueryEntityPropertyGetAllResponse
            {
                Id = c.Id,
                Name = c.Name,
                TypeDataId = c.TypeDataId,
                IsRequired = c.IsRequired,
                PropertyMaxLength = c.PropertyMaxLength,
                IsRequiredRel = c.IsRequiredRel,
                EntityId = c.EntityId,
                TypeRel = c.TypeRel,
                TypeDataDescription = c.TypeDataRel.Name
            }).ToList();
    }
}

