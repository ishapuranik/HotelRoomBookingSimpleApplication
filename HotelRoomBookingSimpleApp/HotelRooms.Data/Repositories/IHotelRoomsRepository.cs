using HotelRooms.Persistence.Models;
using HotelRooms.Persistence.Shared;

namespace HotelRooms.Data
{
    public interface IHotelRoomsRepository : IRepository<HotelRoomsModel, int>
    {
        IList<HotelRoomsModel> GetAllAvailableHotelRooms(DateTime dateTime);
        bool IsRoomAvailable(int roomNumber, DateTime date);
    }
}