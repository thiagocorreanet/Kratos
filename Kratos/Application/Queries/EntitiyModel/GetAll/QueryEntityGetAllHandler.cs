using Application.Notification;
using Core.Abstract;
using Core.Repositories;
using MediatR;

namespace Application.Queries.EntitiyModel.GetAll;

public class QueryEntityGetAllHandler : BaseCQRS, IRequestHandler<QueryEntityGetAllRequest, QueryEntityGetAllResponse>
{
    private readonly IEntityRepository _repository;
    private readonly IProjectRepository _projectRepository;
    public QueryEntityGetAllHandler(INotificationError notificationError, IEntityRepository repository, IProjectRepository projectRepository) : base(notificationError)
    {
        _repository = repository;
        _projectRepository = projectRepository;
    }

    public async Task<QueryEntityGetAllResponse> Handle(QueryEntityGetAllRequest request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        var getAllProject = await _projectRepository.GetAllAsync();
        return request.ToResponse(entities.ToList(), getAllProject.ToList());
    }
}
