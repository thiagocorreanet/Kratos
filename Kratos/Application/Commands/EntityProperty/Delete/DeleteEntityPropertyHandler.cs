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
        bool transactionStarted = false;

        var entityProperty = await _repository.GetByIdAsync(request.Id);

        if (entityProperty is null)
        {
            Notify("Unable to locate the record.");
            return false;
        }

        try
        {
            transactionStarted = await StartTransactionAsync();

            var result = await DeleteEntityPropertyAsync(entityProperty);
            if (!result) return RollbackWithError("Unable to delete the record. Please try again.");

            await CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, transactionStarted);
            return false;
        }
    }
    
    private async Task<bool> StartTransactionAsync()
    {
        await _repository.StartTransactionAsync();
        return true;
    }
    
    private async Task<bool> DeleteEntityPropertyAsync(Core.Entities.EntityProperty entityProperty)
    {
        _repository.Delete(entityProperty);
        return await _repository.SaveChangesAsync();
    }
    
    private async Task CommitTransactionAsync()
    {
        await _repository.CommitTransactionAsync();
    }
    
    private async Task HandleExceptionAsync(Exception ex, bool transactionStarted)
    {
        if (transactionStarted) await _repository.RollbackTransactionAsync();
        _logger.LogCritical(ex, "Error while deleting entity. Details: {Message}", ex.Message);
        Notify("Oops! We were unable to process your request.");
    }
    
    private bool RollbackWithError(string message)
    {
        Notify(message);
        return false;
    }
}