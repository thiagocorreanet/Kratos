using Application.Notification;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllHandler : BaseCQRS, IRequestHandler<QueryEntityGetAllRequest, IEnumerable<QueryEntityGetAllResponse>>
{
    readonly IEntityRepository _repository;

    public QueryEntityGetAllHandler(INotificationError notificationError, IEntityRepository repository) : base(notificationError)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<QueryEntityGetAllResponse>> Handle(QueryEntityGetAllRequest request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return request.ToResponse(entities.ToList());
    }
}
