using Application.Notification;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntityProperty.GetAll;

public class QueryEntityPropertyGetAllHandler : BaseCQRS, IRequestHandler<QueryEntityPropertyGetAllRequest, IEnumerable<QueryEntityPropertyGetAllResponse>>
{
    private readonly IEntityPropertyRepository _repository;

    public QueryEntityPropertyGetAllHandler(INotificationError notificationError, IEntityPropertyRepository repository) : base(notificationError)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<QueryEntityPropertyGetAllResponse>> Handle(QueryEntityPropertyGetAllRequest request, CancellationToken cancellationToken)
    {
        var getList = await _repository.GetAllTypeDataAsync();
        return request.ToResponse(getList.ToList());
    }
}