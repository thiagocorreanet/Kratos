using MediatR;

namespace Application.Queries.Project.GetAll;

public class QueryProjectGetAllRequest : IRequest<IEnumerable<QueryProjectGetAllResponse>>
{
    public List<QueryProjectGetAllResponse> ToResponse(List<Core.Entities.Project> entities)
    {
        List<QueryProjectGetAllResponse> queryProjectGetAllResponse = new List<QueryProjectGetAllResponse>();

        foreach (var item in entities)
        {
            queryProjectGetAllResponse.Add(new QueryProjectGetAllResponse
            {
                Id = item.Id,
                Name = item.Name,
                AlteredAt = item.AlteredAt,
            });
        }

        return queryProjectGetAllResponse;
    }
}