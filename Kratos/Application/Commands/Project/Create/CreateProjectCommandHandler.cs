using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Project.Create;

public class CreateProjectCommandHandler : BaseCQRS, IRequestHandler<CreateProjectCommandRequest, bool>
{

    private readonly IProjectRepository _repository;
    private ILogger<CreateProjectCommandHandler> _logger;

    public CreateProjectCommandHandler(INotificationError notificationError, IProjectRepository repository, ILogger<CreateProjectCommandHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;
        try
        {
            transactionStarted = await StartTransactionAsync();

            var result = await SaveProjectAsync(request);
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

    private async Task<bool> SaveProjectAsync(CreateProjectCommandRequest request)
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