using Application.Commands.Entitie.Delete;
using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Entity.Delete
{
    public class DeleteEntityCommandHandler : BaseCQRS, IRequestHandler<DeleteEntityCommandRequest, bool>
    {
        readonly IEntityRepository _repository;
        readonly ILogger<DeleteEntityCommandHandler> _logger;

        public DeleteEntityCommandHandler(INotificationError notificationError, IEntityRepository repository, ILogger<DeleteEntityCommandHandler> logger) : base(notificationError)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteEntityCommandRequest request, CancellationToken cancellationToken)
        {
            bool transactionStared = true;

            var entitie = await _repository.GetByIdAsync(request.Id);

            if (entitie is null)
            {
                Notify(message: "Unable to locate the record.");
                return false;
            }

            try
            {
                await _repository.StartTransactionAsync();
                _repository.Delete(entitie);

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
