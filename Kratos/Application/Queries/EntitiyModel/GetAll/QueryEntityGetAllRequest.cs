using Application.Queries.EntityModel.GetAll;
using Application.Queries.Project.GetAll;
using Azure.Core;
using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllRequest : IRequest<QueryEntityGetAllResponse>
{
    public QueryEntityGetAllResponse ToResponse(List<Core.Entities.Entity> entities, List<Core.Entities.Project> projects)
    {
        var queryEntityGetAllResponse = new QueryEntityGetAllResponse
        {
            Entities = entities
                .OrderByDescending(entity => entity.AlteredAt) 
                .Select(entity => new EntityResponseItem
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ProjectName = entity.ProjectRel?.Name ?? string.Empty, 
                }).ToList(),

            ProjectRel = projects.Select(project => new QueryProjectGetAllResponse
            {
                Id = project.Id,
                Name = project.Name,
            }).ToList()
        };

        return queryEntityGetAllResponse;
    }

}

