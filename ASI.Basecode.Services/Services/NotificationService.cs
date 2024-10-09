using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this._notificationRepository = notificationRepository;
        }

        public (bool, IEnumerable<Notification>) GetNotifications()
        {
            var notifications = _notificationRepository.ViewNotifications();
            if (notifications != null)
            {
                return (true, notifications);
            }
            return (false, null);
        }

        public void AddNotification(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentException("Notification cannot be null.");
            }

            var newNotification = new Notification
            {
                Title = notification.Title,
                Message = notification.Message,
                DateTimeCreated = DateTime.Now
            };
            _notificationRepository.AddNotification(newNotification);
        }

        public void DeleteNotification(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentException("Notification cannot be null.");
            }

            _notificationRepository.DeleteNotification(notification);
        }

        public void UpdateNotification(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentException("Notification cannot be null.");
            }

            _notificationRepository.UpdateNotification(notification);
        }
    }
}
