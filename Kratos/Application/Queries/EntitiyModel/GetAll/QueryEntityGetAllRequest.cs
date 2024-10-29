using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllRequest : IRequest<IEnumerable<QueryEntityGetAllResponse>>
{}

