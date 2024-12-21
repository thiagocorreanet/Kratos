
using Application.Queries.Project.GetById;

namespace Application.Queries;

public class QueryEntityGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CreatedAtShortDate { get; set; }
    public string AlteredAtShortDate { get; set; }

   
}
