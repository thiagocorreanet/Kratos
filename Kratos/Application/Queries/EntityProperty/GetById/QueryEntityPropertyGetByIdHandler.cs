using Application.Notification;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntityProperty.GetById;

public class QueryEntityPropertyGetByIdHandler : BaseCQRS, IRequestHandler<QueryEntityPropertyGetByIdRequest, QueryEntityPropertyGetByIdResponse>
{
    private readonly IEntityPropertyRepository _repository;
    
    public QueryEntityPropertyGetByIdHandler(INotificationError notificationError, IEntityPropertyRepository repository) : base(notificationError)
    {
        _repository = repository;
    }

    public async Task<QueryEntityPropertyGetByIdResponse> Handle(QueryEntityPropertyGetByIdRequest request, CancellationToken cancellationToken)
    {
        return request.ToResponse(await _repository.GetByIdAsync(request.Id));
    }
}