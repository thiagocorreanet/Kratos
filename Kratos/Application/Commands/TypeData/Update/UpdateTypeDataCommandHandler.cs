using Application.Commands.TypeData.Delete;
using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TypeData.Update;

public class UpdateTypeDataCommandHandler : BaseCQRS, IRequestHandler<UpdateTypeDataCommandRequest, bool>
{
    private readonly ITypeDataRepository _repository;
    private readonly ILogger<DeleteTypeDataCommandHandler> _logger;

    public UpdateTypeDataCommandHandler(INotificationError notificationError, ITypeDataRepository repository, ILogger<DeleteTypeDataCommandHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateTypeDataCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;

        try
        {
            var getTypeData = await _repository.GetByIdAsync(request.Id);

            if (getTypeData is null)
            {
                Notify("Unable to locate the record.");
                return false;
            }

            transactionStarted = await StartTransactionAsync();

            var result = await UpdateTypeDataAsync(getTypeData, request);
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
    
    private async Task<bool> UpdateTypeDataAsync(Core.Entities.TypeData typeData, UpdateTypeDataCommandRequest request)
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