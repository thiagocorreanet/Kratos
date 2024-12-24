using Application.Queries.Project.GetAll;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllResponse
{
   

    public List<QueryEntityGetAllResponseItem> QueryEntityGetAllResponseItem {  get; set; } = new List<QueryEntityGetAllResponseItem>();
    public List<QueryProjectGetAllResponse> ProjectRel {  get; set; } = new List<QueryProjectGetAllResponse>();

}

public class QueryEntityGetAllResponseItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime AlteredAt { get; set; }
}
