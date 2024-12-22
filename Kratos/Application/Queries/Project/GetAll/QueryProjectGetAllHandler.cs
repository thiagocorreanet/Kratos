using Application.Notification;
using Application.Queries.EntitiyModel.GetAll;
using Core.Abstract;
using MediatR;

namespace Application.Queries.Project.GetAll;

public class QueryProjectGetAllHandler : BaseCQRS, IRequestHandler<QueryProjectGetAllRequest, IEnumerable<QueryProjectGetAllResponse>>
{
    private readonly IProjectRepository _repository;

    public QueryProjectGetAllHandler(INotificationError notificationError, IProjectRepository repository) : base(notificationError)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<QueryProjectGetAllResponse>> Handle(QueryProjectGetAllRequest request, CancellationToken cancellationToken)
    {
        var getProjects = await _repository.GetAllAsync();
        getProjects = getProjects.OrderByDescending(x => x.AlteredAt);
        return request.ToResponse(getProjects.ToList());
    }
}