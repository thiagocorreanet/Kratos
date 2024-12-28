namespace Application.Queries.TypeData.GetById;

public class QueryTypeDataGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime AlteredAt { get; set; }

    public string CreatedAtShortDate { get; set; } = null!;
    public string AlteredAtShortDate { get; set; } = null!;
}