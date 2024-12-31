using Application.Commands.Entitie.Delete;
using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Entity.Delete;

public class DeleteEntityCommandHandler : BaseCQRS, IRequestHandler<DeleteEntityCommandRequest, bool>
{
    private readonly IEntityRepository _repository;
    private readonly ILogger<DeleteEntityCommandHandler> _logger;

    public DeleteEntityCommandHandler(
        INotificationError notificationError,
        IEntityRepository repository,
        ILogger<DeleteEntityCommandHandler> logger
    ) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteEntityCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;

        var entity = await _repository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            Notify("Unable to locate the record.");
            return false;
        }

        try
        {
            transactionStarted = await StartTransactionAsync();

            var result = await DeleteEntityAsync(entity);
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

    private async Task<bool> DeleteEntityAsync(Core.Entities.Entity entity)
    {
        _repository.Delete(entity);
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
