using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASI.Basecode.Data.Models;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> ViewBookings();
        void AddBooking(Booking booking);
        (bool, IEnumerable<Booking>) GetBookings();
        IEnumerable<Room> GetRooms();
        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);

    }
}
