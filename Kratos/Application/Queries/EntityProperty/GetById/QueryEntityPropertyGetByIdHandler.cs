using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntityProperty.GetById;

public class QueryEntityPropertyGetByIdHandler : BaseCQRS, IRequestHandler<QueryEntityPropertyGetByIdRequest, QueryEntityPropertyGetByIdResponse>
{
    private readonly IEntityPropertyRepository _repository;
    
    public QueryEntityPropertyGetByIdHandler(INotificationError notificationError, IMapper iMapper, IEntityPropertyRepository repository) : base(notificationError, iMapper)
    {
        _repository = repository;
    }

    public async Task<QueryEntityPropertyGetByIdResponse> Handle(QueryEntityPropertyGetByIdRequest request, CancellationToken cancellationToken)
    {
        return await SimpleMapping<QueryEntityPropertyGetByIdResponse>(await _repository.GetByIdAsync(request.Id));
    }
}