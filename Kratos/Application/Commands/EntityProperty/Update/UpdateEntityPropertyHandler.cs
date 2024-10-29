using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.EntityProperty.Update;

public class UpdateEntityPropertyHandler : BaseCQRS, IRequestHandler<UpdateEntityPropertyRequest, bool>
{
    private readonly IEntityPropertyRepository _repository;
    private readonly ILogger<UpdateEntityPropertyHandler> _logger;
    
    public UpdateEntityPropertyHandler(INotificationError notificationError, IMapper iMapper, ILogger<UpdateEntityPropertyHandler> logger, IEntityPropertyRepository repository) : base(notificationError, iMapper)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateEntityPropertyRequest request, CancellationToken cancellationToken)
    {
        const bool transactionStared = true;

        try
        {
            var entityPropertyById = await _repository.GetByIdAsync(request.Id);

            if (entityPropertyById is null)
            {
                Notify(message: "Unable to locate the record.");
                return false;
            }

            await _repository.StartTransactionAsync();
            _repository.Update(await SimpleMapping<Core.Entities.EntityProperty>(request));
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
            _logger.LogCritical($"ops! We were unable to process your request. Details error: {ex.Message}");
            Notify(message: "Oops! We were unable to process your request.");
        }

        return true;
    }
}