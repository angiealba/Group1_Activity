using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            this._bookingRepository = bookingRepository;
        }

        public (bool, IEnumerable<Booking>) GetBookings()
        {
            var bookings = _bookingRepository.ViewBookings();
            if (bookings != null)
            {
                return (true, bookings);
            }
            return (false, null);
        }

        public void AddBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentException("Booking cannot be null.");
            }

            var newBooking = new Booking();

            newBooking.RoomId = booking.RoomId;
            newBooking.Date = booking.Date;
            newBooking.Time = booking.Time;
            newBooking.Duration = booking.Duration;

            _bookingRepository.AddBooking(newBooking);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _bookingRepository.GetRooms();
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }

        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentException("Booking cannot be null.");
            }

            _bookingRepository.UpdateBooking(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentException("Booking cannot be null.");
            }

            _bookingRepository.DeleteBooking(booking);
        }
    }
}
