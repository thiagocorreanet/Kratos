using MediatR;

namespace Application.Queries.Project.GetAll;

public class QueryProjectGetAllRequest : IRequest<List<QueryProjectGetAllResponse>>
{
    public List<QueryProjectGetAllResponse> ToResponse(List<Core.Entities.Project> projects)
    {

        return projects
            .Select(item => new QueryProjectGetAllResponse
            {
                Id = item.Id,
                Name = item.Name,
                AlteredAt = item.AlteredAt,
            }).OrderBy(e => e.AlteredAt)
            .ToList();
    }
}