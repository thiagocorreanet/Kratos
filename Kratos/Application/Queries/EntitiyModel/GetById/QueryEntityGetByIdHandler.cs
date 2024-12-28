using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Identity.Client;

namespace Application.Queries.EntitiyModel.GetById
{
    public class QueryEntityGetByIdHandler : BaseCQRS, IRequestHandler<QueryEntityGetByIdRequest, QueryEntityGetByIdResponse>
    {
        private readonly IEntityRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITypeDataRepository _typeDataRepository;
        private readonly IEntityPropertyRepository _entityPropertyRepository;

        public QueryEntityGetByIdHandler(INotificationError notificationError, IEntityRepository repository, IProjectRepository projectRepository, ITypeDataRepository typeDataRepository, IEntityPropertyRepository entityPropertyRepository) : base(notificationError)
        {
            _repository = repository;
            _projectRepository = projectRepository;
            _typeDataRepository = typeDataRepository;
            _entityPropertyRepository = entityPropertyRepository;
        }

        public async Task<QueryEntityGetByIdResponse> Handle(QueryEntityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entitieById = await _repository.GetByIdAsync(request.Id);
            var getAllProject = await _projectRepository.GetAllAsync();
            var getAllTypeData = await _typeDataRepository.GetAllAsync();
            var getAllEntities = await _repository.GetAllAsync();
            var getAllEntityProperties = await _entityPropertyRepository.GetAllTypeDataAsync();
            return request.ToResponse(entitieById, getAllProject.ToList(), getAllTypeData.ToList(), getAllEntities.ToList(), getAllEntityProperties.ToList());
        }
    }
}
