using FluentAssertions;
using HotelBooking.Services;
using HotelBooking.Tests.Helpers;
using HotelRooms.Data;
using HotelRooms.Persistence;
using HotelRooms.Persistence.UnitOfWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBooking.Tests.BookingManager
{
    public class BookingManagerTests
    {
        private InMemoryDbContext<HotelDBContext> _inMemoryDbContext;
        private HotelRoomsRepository _hotelRoomsRepository;
        private CustomerRepository _customerRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        public BookingManagerTests()
        {
            SetupInMemoryDB();
            SetupRepositories();
        }

        private void SetupInMemoryDB()
        {
            _inMemoryDbContext = new InMemoryDbContext<HotelDBContext>("HotelDB_InMemory");
            InMemoryDbHelper.PopulateInMemoryDb(_inMemoryDbContext);
        }

        private void SetupRepositories()
        {
            var dbContext = _inMemoryDbContext.GetDbContext();
            _hotelRoomsRepository = new HotelRoomsRepository(dbContext);
            _customerRepository = new CustomerRepository(dbContext);
        }

        [Fact]
        public void AddBooking_throws_exception_if_Room_is_not_available()
        {
            //Given 
            Mock<IBookingService> bookingService = new Mock<IBookingService>();
            bookingService.Setup(x => x.AddBooking(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new Exception("Room is not available"));
            
            // Act
            Action act = () => new HotelBooking.BookingManager(bookingService.Object).AddBooking("xyz", 101, new DateTime(2022, 08, 01));

            // Assert
            act.Should().Throw<Exception>().WithMessage("Room is not available");
        }
        
    }
}
