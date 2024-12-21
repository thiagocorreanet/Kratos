using MediatR;

namespace Application.Queries.Project.GetAll;

public class QueryProjectGetAllRequest : IRequest<IEnumerable<QueryProjectGetAllResponse>>
{
    
}