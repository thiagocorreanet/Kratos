using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Project.Update;

public class UpdateProjectCommandHandler : BaseCQRS, IRequestHandler<UpdateProjectCommandRequest, bool>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(INotificationError notificationError, IProjectRepository repository, ILogger<UpdateProjectCommandHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;

        try
        {
            var getByIdProject = await _repository.GetByIdAsync(request.Id);

            if (getByIdProject is null)
            {
                Notify("Unable to locate the record.");
                return false;
            }

            transactionStarted = await StartTransactionAsync();

            var result = await UpdateProjectAsync(getByIdProject, request);
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

    private async Task<bool> UpdateProjectAsync(Core.Entities.Project project, UpdateProjectCommandRequest request)
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
        _logger.LogCritical(ex, "Error while updating project. Details: {Message}", ex.Message);
        Notify("Oops! We were unable to process your request.");
    }

    private bool RollbackWithError(string message)
    {
        Notify(message);
        return false;
    }
}