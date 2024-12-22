using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.EntityProperty.Delete;

public class DeleteEntityPropertyHandler : BaseCQRS, IRequestHandler<DeleteEntityPropertyRequest, bool>
{
    private IEntityPropertyRepository _repository;
    private ILogger<DeleteEntityPropertyHandler> _logger;

    public DeleteEntityPropertyHandler(INotificationError notificationError, IEntityPropertyRepository repository, ILogger<DeleteEntityPropertyHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteEntityPropertyRequest request, CancellationToken cancellationToken)
    {
        const bool transactionStared = true;

        var entityPropertyById = await _repository.GetByIdAsync(request.Id);

        if (entityPropertyById is null)
        {
            Notify(message: "Unable to locate the record.");
            return false;
        }

        try
        {
            await _repository.StartTransactionAsync();
            _repository.Delete(entityPropertyById);
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