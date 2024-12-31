using Application.Queries.Project.GetAll;

namespace Application.Queries.EntityModel.GetAll;

public class QueryEntityGetAllResponse
{
    public List<EntityResponseItem> Entities { get; set; } = new();
    public List<QueryProjectGetAllResponse> ProjectRel { get; set; } = new();
}

public class EntityResponseItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime AlteredAt { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}