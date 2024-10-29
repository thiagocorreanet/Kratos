using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllHandler : BaseCQRS, IRequestHandler<QueryEntityGetAllRequest, IEnumerable<QueryEntityGetAllResponse>>
{
    readonly IEntityRepository _repository;

    public QueryEntityGetAllHandler(INotificationError notificationError, IMapper iMapper, IEntityRepository repository) : base(notificationError, iMapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<QueryEntityGetAllResponse>> Handle(QueryEntityGetAllRequest request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return await MappingList<QueryEntityGetAllResponse>(entities);
    }
}
