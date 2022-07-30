using HotelBooking.Services;

namespace HotelBooking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingService _bookingService;

        public BookingManager(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void AddBooking(string guest, int room, DateTime date)
        {
           _bookingService.AddBooking(guest, room, date);
        }

        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            return _bookingService.GetAvailableRooms(date);
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            return _bookingService.IsRoomAvailable(room, date);
        }
    }
}