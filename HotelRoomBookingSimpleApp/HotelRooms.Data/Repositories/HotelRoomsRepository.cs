
using HotelRooms.Persistence;
using HotelRooms.Persistence.Models;
using HotelRooms.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace HotelRooms.Data
{
    public class HotelRoomsRepository :
        RepositoryBase<HotelRoomsModel, HotelDBContext, int>,
        IHotelRoomsRepository
    {
        public HotelRoomsRepository(HotelDBContext dbContext)
        : base(dbContext)
        { }

        public IList<HotelRoomsModel> GetAllAvailableHotelRooms(DateTime dateTime)
        {
            var result = Query.Where(x => (!x.BookedDate.HasValue || x.BookedDate.Value.Date != dateTime.Date));

            if (result == null || !result.Any())
                return new List<HotelRoomsModel>();

            return Query.Where(x => (!x.BookedDate.HasValue || x.BookedDate.Value.Date != dateTime.Date)).ToList();
        }

        public bool IsRoomAvailable(int roomNumber, DateTime date)
        {
            if (Query.Where(x => x.RoomNumber == roomNumber && (!x.BookedDate.HasValue || x.BookedDate.Value.Date != date.Date)) != null)
                return Query.Where(x => x.RoomNumber == roomNumber && (!x.BookedDate.HasValue || x.BookedDate.Value.Date != date.Date)).Any();

            return false;
        }
    }
}