using Application.Commands.Entitie.Delete;
using Application.Notification;
using AutoMapper;
using Core.Abstract;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Project.Delete;

public class DeleteProjectCommandHandler : BaseCQRS, IRequestHandler<DeleteProjectCommandRequest, bool>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    public DeleteProjectCommandHandler(INotificationError notificationError, IProjectRepository repository, ILogger<DeleteProjectCommandHandler> logger, IMapper iMapper) : base(notificationError, iMapper)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProjectCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStared = true;

        var getByIdProject = await _repository.GetByIdAsync(request.Id);

        if (getByIdProject is null)
        {
            Notify(message: "Unable to locate the record.");
            return false;
        }

        try
        {
            await _repository.StartTransactionAsync();
            _repository.Delete(getByIdProject);

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