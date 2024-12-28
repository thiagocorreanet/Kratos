using Application.Queries.EntitiyModel.GetAll;

namespace Application.Queries.TypeData.GetAll;

public class QueryTypeDataGetAllResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime AlteredAt { get; set; }
}