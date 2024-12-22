using Application.Notification;
using Core.Abstract;
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
        const bool transactionStared = true;

        try
        {

            await _repository.StartTransactionAsync();

            _repository.Add(request.ToEntity(request));
            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                Notify(message: "Oops! We couldn't save your record. Please try again.");
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