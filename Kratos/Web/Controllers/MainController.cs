using Application.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.Controllers;

public abstract class MainController : Controller
{
    private readonly INotificationError _notificationError;

    protected MainController(INotificationError notificationError)
    {
        _notificationError = notificationError;
    }

    /// <summary>
    /// Verifica se não existem erros registrados
    /// </summary>
    protected bool ValidOperation()
        => _notificationError == null || !_notificationError.HasNotifications();

    /// <summary>
    /// Retorna uma resposta JSON para ser consumida pelo JavaScript
    /// </summary>
    protected JsonResult CustomJsonResponse(object? result = null, bool isValid = false)
    {


        if (isValid is false && ValidOperation())
        {
            return Json(new
            {
                success = true,
                data = result
            });
        }

        return Json(new
        {
            success = false,
            errors = _notificationError?.GetNotifications().Select(n => n.Message)
        });
    }

    /// <summary>
    /// Adiciona mensagens de erro diretamente
    /// </summary>
    protected void NotifyError(string message)
    {
        if (_notificationError != null)
        {
            _notificationError.Handle(new NotificationErrorMessage(message));
        }
        else
        {
            ModelState.AddModelError(string.Empty, message);
        }
    }

    /// <summary>
    /// Valida o ModelState e registra erros se houver
    /// </summary>
    protected void NotifyModelStateErrors(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            foreach (var error in modelState.Values.SelectMany(e => e.Errors))
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMessage);
            }
        }
    }
}
