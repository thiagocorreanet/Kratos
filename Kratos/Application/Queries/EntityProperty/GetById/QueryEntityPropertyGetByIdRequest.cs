using MediatR;

namespace Application.Queries.EntityProperty.GetById;

public class QueryEntityPropertyGetByIdRequest : IRequest<QueryEntityPropertyGetByIdResponse>
{
    public QueryEntityPropertyGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}