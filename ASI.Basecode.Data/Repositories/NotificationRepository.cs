using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASI.Basecode.Data.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        private readonly AsiBasecodeDBContext _dbContext;

        public NotificationRepository(AsiBasecodeDBContext dbContext, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Notification> ViewNotifications()
        {
            return _dbContext.Notification.ToList();
        }

        public void AddNotification(Notification notification)
        {
            try
            {
                var newNotification = new Notification
                {
                    Title = notification.Title,
                    Message = notification.Message,
                    DateTimeCreated = DateTime.Now
                };
                _dbContext.Notification.Add(newNotification);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new InvalidDataException("Error adding notification");
            }
        }

        public void DeleteNotification(Notification notification)
        {
            _dbContext.Notification.Remove(notification);
            _dbContext.SaveChanges();
        }

        public void UpdateNotification(Notification notification)
        {
            var existingNotification = _dbContext.Notification.FirstOrDefault(n => n.NotifId == notification.NotifId);
            if (existingNotification != null)
            {
                existingNotification.Title = notification.Title;
                existingNotification.Message = notification.Message;

                _dbContext.Notification.Update(existingNotification);
                _dbContext.SaveChanges();
            }
        }

        public (bool, IEnumerable<Notification>) GetNotifications()
        {
            throw new NotImplementedException();
        }
    }
}
