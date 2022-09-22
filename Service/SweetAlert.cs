using static VisitorManagement2022.Enum.SweetAlertEnum;

namespace VisitorManagement2022.Service
{
    public class SweetAlert : ISweetAlert
    {
        /// <summary>
        /// SweetAlert popups  https://sweetalert2.github.io/#download https://sweetalert2.github.io/#input-types/// 
        /// </summary>
        public string AlertPopup(string title, string message, NotificationType notificationType)
        {
            return "<script type=\"text/javascript\">Swal.fire({ " + "title: '" + title + "', " + "text: '" + message + "', " + "icon: '" + notificationType.ToString() + "', " + "buttons: false, " + "timer: '10000'})</script>";
        }
        public string AlertCancel(string title, string message, string buttonText, NotificationType notificationType)
        {
            return "<script type=\"text/javascript\">Swal.fire({ " + "title: '" + title + "', " + "text: '" + message + "', " + "icon: '" + notificationType.ToString() + "', " + "button: '" + buttonText + "', " + "timer: '5000'})</script>";
        }
        public string AlertPopupWithImage(string title, string message, NotificationType notificationType)
        {
            return "<script type=\"text/javascript\">Swal.fire({ " + "title: '" + title + "', " + "text: '" + message + "', " + "icon: '" + notificationType.ToString() + "', " + "imageUrl: '" + "/images/logo.png" + "', " + "imageWidth: '" + 200 + "', " + "imageHeight: '" + 200 + "', " + "buttons: false, " + "timer: '5000'})</script>";
        }
    }


}
