using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.TypeData.GetAll;

public class QueryTypeDataGetAllHandler : BaseCQRS, IRequestHandler<QueryTypeDataGetAllRequest, List<QueryTypeDataGetAllResponse>>
{
    
    private readonly ITypeDataRepository _repository;
    private readonly IEntityRepository _repositoryEntity;
    private readonly ILogger<QueryTypeDataGetAllHandler> _logger;

    public QueryTypeDataGetAllHandler(INotificationError notificationError, ITypeDataRepository repository, IEntityRepository repositoryEntity, ILogger<QueryTypeDataGetAllHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _repositoryEntity = repositoryEntity;
        _logger = logger;
    }
  
    public async Task<List<QueryTypeDataGetAllResponse>> Handle(QueryTypeDataGetAllRequest request, CancellationToken cancellationToken)
    {
       var getTypeData = await _repository.GetAllAsync();
       return request.ToResponse(getTypeData.ToList());
    }
}