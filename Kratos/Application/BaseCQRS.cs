using Application.Notification;
using AutoMapper;
using Core.Entities;

using FluentValidation;
using FluentValidation.Results;

namespace Application
{
    public class BaseCQRS
    {
        readonly INotificationError _notificationError;
        private readonly IMapper _iMapper;

        public BaseCQRS(INotificationError notificationError, IMapper iMapper)
        {
            _notificationError = notificationError;
            _iMapper = iMapper;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notificationError.Handle(new NotificationErrorMessage(message));
        }

        protected Task<TEntity> SimpleMapping<TEntity>(object tModel)
        {
            TEntity entity = _iMapper.Map<TEntity>(tModel);
            return Task.FromResult(entity);
        }

        protected Task<IEnumerable<TEntity>> MappingList<TEntity>(IEnumerable<object> tModelList)
        {
            IEnumerable<TEntity> entityList = _iMapper.Map<IEnumerable<TEntity>>(tModelList);
            return Task.FromResult(entityList);
        }
    }
}
