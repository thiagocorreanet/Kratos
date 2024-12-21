using Application.Notification;
using AutoMapper;
using Core.Abstract;
using MediatR;

namespace Application.Queries.Project.GetById;

public class QueryProjectGetByIdHandler : BaseCQRS, IRequestHandler<QueryProjectGetByIdRequest, QueryProjectGetByIdResponse>
{

    private readonly IProjectRepository _repository;

    public QueryProjectGetByIdHandler(INotificationError notificationError, IProjectRepository repository, IMapper imapper) : base(notificationError, imapper)
    {
        _repository = repository;
    }

    public async Task<QueryProjectGetByIdResponse> Handle(QueryProjectGetByIdRequest request, CancellationToken cancellationToken)
    {
        var getProjectById = await _repository.GetByIdAsync(request.Id);
        return request.ToResponse(getProjectById);
    }
}
