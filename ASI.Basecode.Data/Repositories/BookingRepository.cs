using ASI.Basecode.Data.Interfaces;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASI.Basecode.Data.Models;
using System.IO;

namespace ASI.Basecode.Data.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        private readonly AsiBasecodeDBContext _dbContext;

        public BookingRepository(AsiBasecodeDBContext dbContext, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Booking> ViewBookings() 
        {
            return _dbContext.Bookings.ToList();
        }
        public void AddBooking(Booking booking) 
        {
            try
            {
                var newBooking = new Booking();
                newBooking.RoomId = booking.RoomId;
                newBooking.Date = booking.Date;
                newBooking.Time = booking.Time;
                newBooking.Duration = booking.Duration;
                _dbContext.Bookings.Add(newBooking);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new InvalidDataException("error adding booking");
            }
        }
        public  (bool, IEnumerable<Booking>) GetBookings() 
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetRooms()
        {
            return _dbContext.Rooms.ToList();
        }

        public Booking GetBookingById(int id)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(x => x.BookingId == id);

            if (booking == null)
            {
                throw new Exception("Book not found!");
            }

            return booking;
        }

        public void UpdateBooking(Booking booking)
        {
            var existingBooking = GetBookingById(booking.BookingId);

            if (existingBooking != null)
            {
                existingBooking.RoomId = booking.RoomId;
                existingBooking.Date = booking.Date;
                existingBooking.Time = booking.Time;
                existingBooking.Duration = booking.Duration;

                _dbContext.Bookings.Update(existingBooking);
                _dbContext.SaveChanges();

            }
        }

        public void DeleteBooking(Booking booking)
        {
            var existingBooking = GetBookingById(booking.BookingId);
            _dbContext.Bookings.Remove(existingBooking);
            _dbContext.SaveChanges();
        }
    }
}
