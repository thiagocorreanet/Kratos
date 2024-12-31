using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Entity.Update;

public class UpdateEntityCommandHandler : BaseCQRS, IRequestHandler<UpdateEntityCommandRequest, bool>
{
    private readonly IEntityRepository _repository;
    private readonly ILogger<UpdateEntityCommandHandler> _logger;

    public UpdateEntityCommandHandler(
        INotificationError notificationError,
        IEntityRepository repository,
        ILogger<UpdateEntityCommandHandler> logger
    ) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateEntityCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;

        try
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity is null)
            {
                Notify("Unable to locate the record.");
                return false;
            }

            transactionStarted = await StartTransactionAsync();

            var result = await UpdateEntityAsync(entity, request);
            if (!result) return RollbackWithError("Unable to update the record. Please try again.");

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

    private async Task<bool> UpdateEntityAsync(Core.Entities.Entity entity, UpdateEntityCommandRequest request)
    {
        _repository.Update(request.ToEntity(request));
        return await _repository.SaveChangesAsync();
    }

    private async Task CommitTransactionAsync()
    {
        await _repository.CommitTransactionAsync();
    }

    private async Task HandleExceptionAsync(Exception ex, bool transactionStarted)
    {
        if (transactionStarted) await _repository.RollbackTransactionAsync();
        _logger.LogCritical(ex, "Error while updating entity. Details: {Message}", ex.Message);
        Notify("Oops! We were unable to process your request.");
    }

    private bool RollbackWithError(string message)
    {
        Notify(message);
        return false;
    }
}
