using HotelRooms.Persistence.Models;
using HotelRooms.Persistence.Shared;

namespace HotelRooms.Data
{
    public interface ICustomerRepository : IRepository<CustomerModel, int>
    {
        CustomerModel? GetCustomer(string name);
    }
}