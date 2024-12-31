﻿using Application.Commands.Entitie.Delete;
using Application.Notification;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Project.Delete;

public class DeleteProjectCommandHandler : BaseCQRS, IRequestHandler<DeleteProjectCommandRequest, bool>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    public DeleteProjectCommandHandler(INotificationError notificationError, IProjectRepository repository, ILogger<DeleteProjectCommandHandler> logger) : base(notificationError)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProjectCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = false;

        var getProjectById = await _repository.GetByIdAsync(request.Id);

        if (getProjectById is null)
        {
            Notify("Unable to locate the record.");
            return false;
        }

        try
        {
            transactionStarted = await StartTransactionAsync();

            var result = await DeleteProjectAsync(getProjectById);
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

    private async Task<bool> DeleteProjectAsync(Core.Entities.Project project)
    {
        _repository.Delete(project);
        return await _repository.SaveChangesAsync();
    }

    private async Task CommitTransactionAsync()
    {
        await _repository.CommitTransactionAsync();
    }

    private async Task HandleExceptionAsync(Exception ex, bool transactionStarted)
    {
        if (transactionStarted) await _repository.RollbackTransactionAsync();
        _logger.LogCritical(ex, "Error while deleting project. Details: {Message}", ex.Message);
        Notify("Oops! We were unable to process your request.");
    }

    private bool RollbackWithError(string message)
    {
        Notify(message);
        return false;
    }
}