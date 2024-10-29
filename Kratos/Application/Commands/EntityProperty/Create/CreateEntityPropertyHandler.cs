using Application.Commands.Entity.Create;
using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Application.Commands.EntityProperty.Create;

public class CreateEntityPropertyHandler : BaseCQRS, IRequestHandler<CreateEntityPropertyRequest, bool>
{
    private readonly IEntityPropertyRepository _repository;
    private readonly ILogger<CreateEntityCommandHandler> _logger;

    public CreateEntityPropertyHandler(INotificationError notificationError, IMapper iMapper,
        IEntityPropertyRepository repository, ILogger<CreateEntityCommandHandler> logger) : base(notificationError,
        iMapper)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateEntityPropertyRequest request, CancellationToken cancellationToken)
    {
        const bool transactionStared = true;

        try
        {
            await _repository.StartTransactionAsync();

            foreach (var item in request.Items)
            {
                _repository.Add(await SimpleMapping<Core.Entities.EntityProperty>(item));
            }
            
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

            _logger.LogCritical(
                message: "Ops! We were unable to process your request. Details error: {ErrorMessage}",
                args: ex.Message);

            Notify(message: "Oops! We were unable to process your request.");
        }

        return true;
    }
}