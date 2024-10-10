using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: BookingController
        public ActionResult Index(string search)
        {
            (bool result, IEnumerable<Booking> bookings) = _bookingService.GetBookings();
            var rooms = _bookingService.GetRooms();
            ViewBag.Rooms = new SelectList(rooms, "roomId", "roomName");

            if (!result)
            {
                return View(null);
            }


            if (!string.IsNullOrEmpty(search))
            {
                bookings = bookings.Where(b => b.Room.roomName.Contains(search, StringComparison.OrdinalIgnoreCase) 
                                            || b.Date.ToString("MM-dd-yyyy").Contains(search)
                                            || b.Time.ToString("HH:mm").Contains(search));
            }
            
            return View(bookings.ToList());
        }

        // POST: BookingController/CreateBooking
        [HttpPost]
        public IActionResult CreateBooking(Booking booking)
        {
            try
            {
                _bookingService.AddBooking(booking);
                return RedirectToAction("Index");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(booking);
            }
        }

        // POST: BookingController/EditBooking
        [HttpPost]
        public IActionResult EditBooking(Booking booking)
        {
            _bookingService.UpdateBooking(booking);
            return RedirectToAction("Index");
        }

        // POST: BookingController/DeleteBooking
        [HttpPost]
        public IActionResult DeleteBooking(Booking booking)
        {
            _bookingService.DeleteBooking(booking);
            return RedirectToAction("Index");
        }
    }
}
