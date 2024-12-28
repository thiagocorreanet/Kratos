using Application.Queries.EntityModel.GetAll;
using Application.Queries.EntityProperty.GetAll;
using Application.Queries.Project.GetAll;
using Application.Queries.Project.GetById;
using Application.Queries.TypeData.GetAll;

namespace Application.Queries;

public class QueryEntityGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CreatedAtShortDate { get; set; } = null!;
    public string AlteredAtShortDate { get; set; } = null!;
    public int ProjectId { get; set; }
    public string? TypeDataName { get; set; }
    public List<QueryProjectGetAllResponse> ProjectRel { get; set; } = new List<QueryProjectGetAllResponse>();
    public List<QueryTypeDataGetAllResponse> TypeDataRel { get; set; } = new List<QueryTypeDataGetAllResponse>();
    public List<EntityResponseItem> EntitiesRel { get; set; } = new List<EntityResponseItem>();
    public List<QueryEntityPropertyGetAllResponse> EntitiesPropertiesRel { get; set; } = new List<QueryEntityPropertyGetAllResponse>();
}