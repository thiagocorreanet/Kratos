using Application.Notification;
using FluentValidation.Results;

namespace Application
{
    public class BaseCQRS
    {
        readonly INotificationError _notificationError;

        public BaseCQRS(INotificationError notificationError)
        {
            _notificationError = notificationError;
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
    }
}
