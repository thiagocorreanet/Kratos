using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllRequest : IRequest<IEnumerable<QueryEntityGetAllResponse>>
{
    public List<QueryEntityGetAllResponse> ToResponse(List<Core.Entities.Entity> entity)
    {
        List<QueryEntityGetAllResponse> queryEntityGetAllResponse = new List<QueryEntityGetAllResponse>();

        foreach (var item in entity)
        {
            queryEntityGetAllResponse.Add(new QueryEntityGetAllResponse
            {
                Id = item.Id,
                Name = item.Name,
            });
        }

        return queryEntityGetAllResponse;
    }
}

