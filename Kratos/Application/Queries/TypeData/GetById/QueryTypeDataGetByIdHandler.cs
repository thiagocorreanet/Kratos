using Application.Notification;
using Application.Queries.TypeData.GetAll;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.TypeData.GetById;

public class QueryTypeDataGetByIdHandler : BaseCQRS, IRequestHandler<QueryTypeDataGetByIdRequest, QueryTypeDataGetByIdResponse>
{
    private readonly ITypeDataRepository _repository;
    private readonly ILogger<QueryTypeDataGetByIdHandler> _logger;

    public QueryTypeDataGetByIdHandler(INotificationError notificationError, ITypeDataRepository repository, ILogger<QueryTypeDataGetByIdHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<QueryTypeDataGetByIdResponse> Handle(QueryTypeDataGetByIdRequest request, CancellationToken cancellationToken)
    {
        var getTypeDataById = await _repository.GetByIdAsync(request.Id);
        return request.ToResponse(getTypeDataById);
    }
}