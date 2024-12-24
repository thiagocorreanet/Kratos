
using Application.Notification;
using Core.Abstract;
using Core.Repositories;

using MediatR;

namespace Application.Queries.Entitie.GetById
{
    public class QueryEntityGetByIdHandler : BaseCQRS, IRequestHandler<QueryEntityGetByIdRequest, QueryEntityGetByIdResponse>
    {
        private readonly IEntityRepository _repository;
        private readonly IProjectRepository _projectRepository;

        public QueryEntityGetByIdHandler(INotificationError notificationError, IEntityRepository repository, IProjectRepository projectRepository) : base(notificationError)
        {
            _repository = repository;
            _projectRepository = projectRepository;
        }

        public async Task<QueryEntityGetByIdResponse> Handle(QueryEntityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entitieById = await _repository.GetByIdAsync(request.Id);
            var getAllProject = await _projectRepository.GetAllAsync();
            return request.ToResponse(entitieById, getAllProject.ToList());
        }
    }
}
