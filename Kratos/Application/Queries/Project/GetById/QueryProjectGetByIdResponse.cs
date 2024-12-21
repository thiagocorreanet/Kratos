namespace Application.Queries.Project.GetById;
public class QueryProjectGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime AlteredAt { get; set; }

    public string CreatedAtShortDate { get; set; }
    public string AlteredAtShortDate { get; set; }
}
