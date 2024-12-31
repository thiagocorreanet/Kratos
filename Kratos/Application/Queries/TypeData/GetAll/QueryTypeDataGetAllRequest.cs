using Application.Queries.EntitiyModel.GetAll;
using MediatR;

namespace Application.Queries.TypeData.GetAll;

public class QueryTypeDataGetAllRequest : IRequest<List<QueryTypeDataGetAllResponse>>
{
    public List<QueryTypeDataGetAllResponse> ToResponse(List<Core.Entities.TypeData> typeData)
    {
        return typeData
            .Select(item => new QueryTypeDataGetAllResponse
            {
                Id = item.Id,
                Name = item.Name,
                AlteredAt = item.AlteredAt,
            })
            .OrderBy(e => e.AlteredAt)
            .ToList();
    }


}

