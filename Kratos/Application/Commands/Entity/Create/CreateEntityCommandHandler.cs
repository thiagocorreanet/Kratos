using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Entity.Create
{
    public class CreateEntityCommandHandler(
        INotificationError notificationError,
        IEntityRepository repository,
        ILogger<CreateEntityCommandHandler> logger,
        IMapper iMapper)
        : BaseCQRS(notificationError, iMapper), IRequestHandler<CreateEntityCommandRequest, bool>
    {
        public async Task<bool> Handle(CreateEntityCommandRequest request, CancellationToken cancellationToken)
        {
            const bool transactionStared = true;

            try
            {
                await repository.StartTransactionAsync();
                repository.Add(await SimpleMapping<Core.Entities.Entity>(request));
                var result = await repository.SaveChangesAsync();

                if (!result)
                {
                    Notify(message: "Oops! We couldn't save your record. Please try again.");
                    await repository.RollbackTransactionAsync();
                    return false;
                }

                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                if (transactionStared) await repository.RollbackTransactionAsync();
                
                logger.LogCritical(
                    message: "Ops! We were unable to process your request. Details error: {ErrorMessage}",
                    args: ex.Message);
                
                Notify(message: "Oops! We were unable to process your request.");
            }

            return true;
        }
    }
}