using Application.Queries.EntityModel.GetAll;
using Application.Queries.EntityProperty.GetAll;
using Application.Queries.Project.GetAll;
using Application.Queries.TypeData.GetAll;
using MediatR;

namespace Application.Queries.EntitiyModel.GetById;

public class QueryEntityGetByIdRequest : IRequest<QueryEntityGetByIdResponse>
{
    public QueryEntityGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public QueryEntityGetByIdResponse ToResponse(Core.Entities.Entity entity, List<Core.Entities.Project> listProjects, List<Core.Entities.TypeData> listTypes, List<Core.Entities.Entity> entities, List<Core.Entities.EntityProperty> entitiesProperties)
    {
        QueryEntityGetByIdResponse response = new QueryEntityGetByIdResponse();

        response.Id = Id;
        response.Name = entity.Name;
        response.CreatedAtShortDate = entity.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
        response.AlteredAtShortDate = entity.AlteredAt.ToString("dd/MM/yyyy HH:mm:ss");
        response.ProjectId = entity.ProjectId;
  
        response.ProjectRel = listProjects.Where(x => x.Id != response.ProjectId)
            .Select(x => new QueryProjectGetAllResponse
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        
        response.TypeDataRel = listTypes.Select(x => new QueryTypeDataGetAllResponse
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();

        response.EntitiesRel = entities.Where(x => x.Id != response.Id)
            .Select(x => new EntityResponseItem
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

        response.EntitiesPropertiesRel = entitiesProperties.Select(x => new QueryEntityPropertyGetAllResponse
        {
            Id = x.Id,
            Name = x.Name,
            TypeDataId = x.TypeDataId,
            IsRequired = x.IsRequired,
            PropertyMaxLength = x.PropertyMaxLength,
            IsRequiredRel = x.IsRequiredRel,
            EntityId = x.EntityId,
            TypeRel = x.TypeRel,
            TypeDataDescription = x.TypeDataRel?.Name ?? string.Empty,
            EntityDescription = x.EntityRel?.Name ?? string.Empty,
        }).ToList();
        
        return response;
    }
}
