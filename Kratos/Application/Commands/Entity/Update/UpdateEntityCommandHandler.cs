using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Entity.Update
{
    public class UpdateEntityCommandHandler : BaseCQRS, IRequestHandler<UpdateEntityCommandRequest, bool>
    {
        private readonly IEntityRepository _repository;
        private readonly ILogger<UpdateEntityCommandHandler> _logger;

        public UpdateEntityCommandHandler(INotificationError notificationError, IEntityRepository repository, ILogger<UpdateEntityCommandHandler> looger, IMapper mapper) : base(notificationError, mapper)
        {
            _repository = repository;
            _logger = looger;
        }

        public async Task<bool> Handle(UpdateEntityCommandRequest request, CancellationToken cancellationToken)
        {
            var transactionStared = true;

            try
            {
                var entitie = await _repository.GetByIdAsync(request.Id);

                if (entitie is null)
                {
                    Notify(message: "Unable to locate the record.");
                    return false;
                }

                await _repository.StartTransactionAsync();
                _repository.Update(await SimpleMapping<Core.Entities.Entity>(request));
                var result = await _repository.SaveChangesAsync();

                if (!result)
                {
                    Notify("Oops! We couldn't save your record. Please try again.");
                    await _repository.RollbackTransactionAsync();
                    return false;
                }

                await _repository.CommitTransactionAsync();

            }
            catch (Exception ex)
            {
                 if (transactionStared) await _repository.RollbackTransactionAsync();
                _logger.LogCritical($"ops! We were unable to process your request. Details error: {ex.Message}");
                Notify(message: "Oops! We were unable to process your request.");
            }

            return true;
        }
    }
}