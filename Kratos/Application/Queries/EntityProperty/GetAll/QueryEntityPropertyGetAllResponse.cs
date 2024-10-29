namespace Application.Queries.EntityProperty.GetAll;

public class QueryEntityPropertyGetAllResponse
{
    public int Id { get; init; } 
    public string Name { get; init; } = null!;
    public string Type { get; init; } = null!;
    public bool IsRequired { get; init; }
    public int EntityId { get; init; }
}