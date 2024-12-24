using Application.Queries.Project.GetAll;
using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllRequest : IRequest<QueryEntityGetAllResponse>
{
    public QueryEntityGetAllResponse ToResponse(List<Core.Entities.Entity> entity, List<Core.Entities.Project> entityProject)
    {
        QueryEntityGetAllResponse queryEntityGetAllResponse = new QueryEntityGetAllResponse();

        foreach (var item in entity)
        {
            queryEntityGetAllResponse.QueryEntityGetAllResponseItem.Add(new QueryEntityGetAllResponseItem
            {
                Id = item.Id,
                Name = item.Name,
            });
        }

        foreach (var project in entityProject)
        {
            queryEntityGetAllResponse.ProjectRel.Add(new QueryProjectGetAllResponse
            {
                Id = project.Id,
                Name = project.Name,
            });
        }

        return queryEntityGetAllResponse;
    }
}

