using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TypeData.Create;

public class CreateTypeDataCommandHandler : BaseCQRS, IRequestHandler<CreateTypeDataCommandRequest, bool>
{
    private readonly ITypeDataRepository _repository;
    private ILogger<CreateTypeDataCommandHandler> _logger;

    public CreateTypeDataCommandHandler(INotificationError notificationError, ITypeDataRepository repository, ILogger<CreateTypeDataCommandHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateTypeDataCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;
        
        try
        {

            transactionStarted = await StartTransactionAsync();

            var result = await SaveEntityAsync(request);
            if (!result)
            {
                await RollbackTransactionAsync();
                Notify("Couldn't save your record. Please try again.");
                return false;
            }

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
    
    private async Task<bool> SaveEntityAsync(CreateTypeDataCommandRequest request)
    {
        _repository.Add(request.ToEntity(request));
        return await _repository.SaveChangesAsync();
    }
    
    private async Task CommitTransactionAsync()
    {
        await _repository.CommitTransactionAsync();
    }
    
    private async Task RollbackTransactionAsync()
    {
        await _repository.RollbackTransactionAsync();
    }
    
    private async Task HandleExceptionAsync(Exception ex, bool transactionStarted)
    {
        if (transactionStarted) await RollbackTransactionAsync();
        _logger.LogCritical(ex, "Error during CreateEntityCommandHandler execution.");
        Notify("An unexpected error occurred. Please contact support.");
    }
}