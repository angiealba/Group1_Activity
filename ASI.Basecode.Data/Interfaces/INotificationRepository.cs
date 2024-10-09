using ASI.Basecode.Data.Models;
using System.Collections.Generic;

namespace ASI.Basecode.Data.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> ViewNotifications();
        void AddNotification(Notification notification);
        void DeleteNotification(Notification notification);
        void UpdateNotification(Notification notification);
        (bool, IEnumerable<Notification>) GetNotifications();
    }
}
