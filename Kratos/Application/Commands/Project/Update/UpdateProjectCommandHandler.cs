using Application.Notification;
using AutoMapper;
using Core.Abstract;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Project.Update;

public class UpdateProjectCommandHandler : BaseCQRS, IRequestHandler<UpdateProjectCommandRequest, bool>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(INotificationError notificationError, IProjectRepository repository, ILogger<UpdateProjectCommandHandler> logger, IMapper iMapper) : base(notificationError, iMapper)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        const bool transactionStared = true;
        try
        {
            var getByIdProject = await _repository.GetByIdAsync(request.Id);
            if (getByIdProject is null)
            {
                Notify(message: "Unable to locate the record.");
                return false;
            }

            await _repository.StartTransactionAsync();

            _repository.Update(await SimpleMapping<Core.Entities.Project>(request));
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
            _logger.LogCritical("Ops! We were unable to process your request.Details error: { ErrorMessage}", ex.Message);
            Notify("Oops! We were unable to process your request.");
        }

        return true;
    }
}