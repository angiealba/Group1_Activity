using ASI.Basecode.Data.Models;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Interfaces
{
    public interface INotificationService
    {
        (bool result, IEnumerable<Notification> notifications) GetNotifications();

        void AddNotification(Notification notification);
        void UpdateNotification(Notification notification);
        void DeleteNotification(Notification notification);
    }
}
