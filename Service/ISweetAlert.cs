using VisitorManagement2022.Enum;

namespace VisitorManagement2022.Service
{
    public interface ISweetAlert
    {
        string AlertCancel(string title, string message, string buttonText, SweetAlertEnum.NotificationType notificationType);
        string AlertPopup(string title, string message, SweetAlertEnum.NotificationType notificationType);
        string AlertPopupWithImage(string title, string message, SweetAlertEnum.NotificationType notificationType);
    }
}