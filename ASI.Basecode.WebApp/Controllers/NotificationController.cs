using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Ensure this is included to use LINQ methods
using System;

namespace ASI.Basecode.WebApp.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IActionResult Index(string search)
        {
            (bool result, IEnumerable<Notification> notifications) = _notificationService.GetNotifications();

            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to retrieve notifications.";
                return View(null);
            }

            if (!string.IsNullOrEmpty(search))
            {
                notifications = notifications.Where(n => n.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                                         n.Message.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(notifications.ToList());
        }

        public IActionResult CreateNotification()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNotification(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid notification data.";
                return View(notification);
            }

            try
            {
                _notificationService.AddNotification(notification);
                TempData["SuccessMessage"] = "Notification created successfully!";
                return RedirectToAction("Index");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(notification);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the notification.";
                return View(notification);
            }
        }

        public IActionResult Delete(int notifId)
        {
            (bool result, IEnumerable<Notification> notifications) = _notificationService.GetNotifications();
            var notification = notifications.FirstOrDefault(n => notifId == n.NotifId);
            if (notification == null)
            {
                TempData["ErrorMessage"] = "Notification not found.";
                return RedirectToAction("Index");
            }

            return View(notification); // Renders a confirmation view for deleting the notification.
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int notifId)
        {
            (bool result, IEnumerable<Notification> notifications) = _notificationService.GetNotifications();
            var notification = notifications.FirstOrDefault(n => notifId == n.NotifId);
            if (notification != null)
            {
                try
                {
                    _notificationService.DeleteNotification(notification);
                    TempData["SuccessMessage"] = "Notification deleted successfully!";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while deleting the notification.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Notification not found.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int notifId)
        {
            (bool result, IEnumerable<Notification> notifications) = _notificationService.GetNotifications();
            var notification = notifications.FirstOrDefault(n => notifId == n.NotifId);
            if (notification == null)
            {
                TempData["ErrorMessage"] = "Notification not found.";
                return NotFound();
            }
            return View(notification);
        }

        [HttpPost]
        public IActionResult Update(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid notification data.";
                return View(notification);
            }

            try
            {
                _notificationService.UpdateNotification(notification);
                TempData["SuccessMessage"] = "Notification updated successfully!";
                return RedirectToAction("Index");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(notification);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the notification.";
                return View(notification);
            }
        }
    }
}
