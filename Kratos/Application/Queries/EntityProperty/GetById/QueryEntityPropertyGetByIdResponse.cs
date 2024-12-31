using Application.Queries.TypeData.GetAll;

namespace Application.Queries.EntityProperty.GetById;

public class QueryEntityPropertyGetByIdResponse
{
    public int Id { get; init; } 
    public string Name { get; init; } = null!;
    public int TypeDataId { get; init; } 
    public bool IsRequired { get; init; }
    public int? EntityId { get; init; }
    public bool IsRequiredRel { get; init; }
    public List<QueryTypeDataGetAllResponse> TypeDataRel {  get; set; } = new List<QueryTypeDataGetAllResponse>();
}