using Application.Queries.TypeData.GetAll;

namespace Application.Queries.EntityProperty.GetAll;

public class QueryEntityPropertyGetAllResponse
{
    public int Id { get; set; } 
    public string Name { get; set; } = null!;
    public int TypeDataId { get;  set; } 
    public bool IsRequired { get;  set; }
    public int PropertyMaxLength { get;  set; }
    public bool IsRequiredRel { get;  set; }
    public int? EntityId { get;  set; }
    public string TypeRel { get;  set; } = null!;
    public string TypeDataDescription { get;  set; } = null!;
    public string EntityDescription { get;  set; } = null!;
    public List<QueryTypeDataGetAllResponse> TypeDataRel { get; set; } = new();
}