using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TypeData.Delete;

public class DeleteTypeDataCommandHandler : BaseCQRS, IRequestHandler<DeleteTypeDataCommandRequest, bool>
{
    private readonly ITypeDataRepository _repository;
    private readonly ILogger<DeleteTypeDataCommandHandler> _logger;

    public DeleteTypeDataCommandHandler(INotificationError notificationError, ITypeDataRepository repository, ILogger<DeleteTypeDataCommandHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTypeDataCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;

        var typeData = await _repository.GetByIdAsync(request.Id);

        if (typeData is null)
        {
            Notify("Unable to locate the record.");
            return false;
        }

        try
        {
            transactionStarted = await StartTransactionAsync();

            var result = await DeleteTypeDataAsync(typeData);
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
    
    private async Task<bool> DeleteTypeDataAsync(Core.Entities.TypeData typeData)
    {
        _repository.Delete(typeData);
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