using Application.Notification;
using Application.Queries.EntitiyModel.GetAll;
using AutoMapper;
using Core.Abstract;
using MediatR;

namespace Application.Queries.Project.GetAll;

public class QueryProjectGetAllHandler : BaseCQRS, IRequestHandler<QueryProjectGetAllRequest, IEnumerable<QueryProjectGetAllResponse>>
{
    private readonly IProjectRepository _repository;

    public QueryProjectGetAllHandler(INotificationError notificationError, IProjectRepository repository, IMapper iMapper) : base(notificationError, iMapper)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<QueryProjectGetAllResponse>> Handle(QueryProjectGetAllRequest request, CancellationToken cancellationToken)
    {
        var getProjects = await _repository.GetAllAsync();
        return await MappingList<QueryProjectGetAllResponse>(getProjects);
    }
}