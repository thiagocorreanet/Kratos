
using Application.Notification;

using AutoMapper;

using Core.Repositories;

using MediatR;

namespace Application.Queries.Entitie.GetById
{
    public class QueryEntityGetByIdHandler : BaseCQRS, IRequestHandler<QueryEntityGetByIdRequest, QueryEntityGetByIdResponse>
    {
        private readonly IEntityRepository _repository;

        public QueryEntityGetByIdHandler(INotificationError notificationError, IMapper iMapper, IEntityRepository repository) : base(notificationError, iMapper)
        {
            _repository = repository;
        }

        public async Task<QueryEntityGetByIdResponse> Handle(QueryEntityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entitieById = await _repository.GetByIdAsync(request.Id);
            return await SimpleMapping<QueryEntityGetByIdResponse>(entitieById);
        }
    }
}
