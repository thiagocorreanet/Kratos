
using Application.Notification;


using Core.Repositories;

using MediatR;

namespace Application.Queries.Entitie.GetById
{
    public class QueryEntityGetByIdHandler : BaseCQRS, IRequestHandler<QueryEntityGetByIdRequest, QueryEntityGetByIdResponse>
    {
        private readonly IEntityRepository _repository;

        public QueryEntityGetByIdHandler(INotificationError notificationError, IEntityRepository repository) : base(notificationError)
        {
            _repository = repository;
        }

        public async Task<QueryEntityGetByIdResponse> Handle(QueryEntityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entitieById = await _repository.GetByIdAsync(request.Id);
            return request.ToResponse(entitieById);
        }
    }
}
