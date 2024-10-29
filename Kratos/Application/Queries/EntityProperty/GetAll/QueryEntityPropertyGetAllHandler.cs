using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntityProperty.GetAll;

public class QueryEntityPropertyGetAllHandler : BaseCQRS, IRequestHandler<QueryEntityPropertyGetAllRequest, IEnumerable<QueryEntityPropertyGetAllResponse>>
{
    private readonly IEntityPropertyRepository _repository;
    
    public QueryEntityPropertyGetAllHandler(INotificationError notificationError, IMapper iMapper, IEntityPropertyRepository repository) : base(notificationError, iMapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<QueryEntityPropertyGetAllResponse>> Handle(QueryEntityPropertyGetAllRequest request, CancellationToken cancellationToken)
    {
        return await MappingList<QueryEntityPropertyGetAllResponse>(await _repository.GetAllAsync());
    }
}