using HotelRooms.Persistence;
using HotelRooms.Persistence.Models;

namespace HotelBooking.Tests.Helpers
{
    public static class InMemoryDbHelper
    {
        private static void AddHotelRoomEntities(InMemoryDbContext<HotelDBContext> context)
        {
            context.AddEntities<HotelRoomsModel>(new List<HotelRoomsModel>() {
                new HotelRoomsModel()
                {
                    Id = 1,
                    RoomNumber = 101,
                    CustomerId = 1001,
                    BookedDate = new DateTime(2022, 08, 01),
                    IsAvailable = false
                }
                ,new HotelRoomsModel()
                {
                    Id = 2,
                    RoomNumber = 204,
                    CustomerId = null,
                    BookedDate = null,
                    IsAvailable = true
                }
                ,new HotelRoomsModel()
                {
                    Id = 3,
                    RoomNumber = 304,
                    CustomerId = null,
                    BookedDate = null,
                    IsAvailable = true
                }
                ,new HotelRoomsModel()
                {
                    Id = 4,
                    RoomNumber = 404,
                    CustomerId = 2002,
                    BookedDate = new DateTime(2022, 08, 03),
                    IsAvailable = false
                }
            });
        }

        private static void AddCustomerEntities(InMemoryDbContext<HotelDBContext> context)
        {
            context.AddEntities<CustomerModel>(new List<CustomerModel>() {
                new CustomerModel()
                {
                    Id = 1,
                    CustomerName = "Surname 4"
                }
                ,new CustomerModel()
                {
                    Id = 2,
                    CustomerName = "Surname 1"
                }
                ,new CustomerModel()
                {
                    Id = 3,
                    CustomerName = "Surname 2"
                }
                ,new CustomerModel()
                {
                    Id = 4,
                    CustomerName = "Surname 3"
                }
            });
        }

        public static void PopulateInMemoryDb(InMemoryDbContext<HotelDBContext> inMemoryDbContext)
        {
            AddHotelRoomEntities(inMemoryDbContext);
            AddCustomerEntities(inMemoryDbContext);
        }


    }
}
