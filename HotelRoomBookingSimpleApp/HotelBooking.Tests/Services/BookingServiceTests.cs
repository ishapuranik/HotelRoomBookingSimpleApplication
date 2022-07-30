using FluentAssertions;
using HotelBooking.Services;
using HotelBooking.Tests.Helpers;
using HotelRooms.Data;
using HotelRooms.Persistence;
using HotelRooms.Persistence.Persistence;
using Xunit;
using Moq;
using HotelRooms.Persistence.UnitOfWork;

namespace HotelBooking.Tests.Services
{
    public class BookingServiceTests
    {
        private InMemoryDbContext<HotelDBContext> _inMemoryDbContext;
        private HotelRoomsRepository _hotelRoomsRepository;
        private CustomerRepository _customerRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        public BookingServiceTests()
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
        public void GetAvailableRooms_returns_valid_data_for_given_date()
        {
            var service = new BookingService(_hotelRoomsRepository, _customerRepository, null);

            // Act
            var result = service.GetAvailableRooms(new DateTime(2022, 09, 01));

            // Assert
            result.Should().HaveCount(4);
        }

        [Fact]
        public void GetAvailableRooms_returns_zero_records_for_given_date_if_data_not_available()
        {
            var service = new BookingService(_hotelRoomsRepository, _customerRepository, null);

            // Act
            var result = service.GetAvailableRooms(new DateTime(2022, 08, 03));

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void IsRoomAvailable_returns_false_if_room_is_not_found_for_given_date()
        {
            var service = new BookingService(_hotelRoomsRepository, _customerRepository, null);

            // Act
            var result = service.IsRoomAvailable(101, new DateTime(2022, 08, 01));

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsRoomAvailable_returns_true_if_room_is_found_on_given_date()
        {
            var service = new BookingService(_hotelRoomsRepository, _customerRepository, null);

            // Act
            var result = service.IsRoomAvailable(101, new DateTime(2022, 11, 01));

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void AddBooking_successfully_adds_new_record()
        {
            var service = new BookingService(_hotelRoomsRepository, _customerRepository, _unitOfWork.Object);

            // Act
            service.AddBooking("Surname 2", 204, new DateTime(2022, 11, 01));

            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        /*
        [Fact]
        public void AddBooking_throws_exception_if_Room_is_not_available()
        {
            // Act
            Action act = () => new BookingService(_hotelRoomsRepository, _customerRepository, _unitOfWork.Object).AddBooking("xyz", 101, new DateTime(2022, 08, 01));

            // Assert
            act.Should().Throw<Exception>().WithMessage("Room is not available");
        }
        */
    }

}
