using MediatR;

namespace Application.Queries.EntitiyModel.GetByIdProperties;

public class QueryEntityGetByIdPropertiesRequest : IRequest<string>
{
    public QueryEntityGetByIdPropertiesRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
