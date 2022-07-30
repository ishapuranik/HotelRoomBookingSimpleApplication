using HotelRooms.Data;
using HotelRooms.Persistence.UnitOfWork;

namespace HotelBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IHotelRoomsRepository _hotelRoomsRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IHotelRoomsRepository hotelRoomsRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _hotelRoomsRepository = hotelRoomsRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async void AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                if (guest == null)
                    throw new ArgumentNullException(nameof(guest));

                if (!IsRoomAvailable(room, date))
                    throw new Exception("Room is not available");

                var customer = _customerRepository.GetCustomer(guest);

                if (customer == null)
                    throw new KeyNotFoundException(nameof(guest));

                _hotelRoomsRepository.Add(new HotelRooms.Persistence.Models.HotelRoomsModel() { BookedDate = date, RoomNumber = room, CustomerId = customer.Id, IsAvailable = false });
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            return _hotelRoomsRepository.GetAllAvailableHotelRooms(date).Select(x => x.RoomNumber);
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            return _hotelRoomsRepository.IsRoomAvailable(room, date);
        }
    }
}
