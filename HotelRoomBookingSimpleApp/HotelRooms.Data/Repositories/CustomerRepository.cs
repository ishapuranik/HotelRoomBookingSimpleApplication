using HotelRooms.Persistence;
using HotelRooms.Persistence.Models;
using HotelRooms.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace HotelRooms.Data
{
    public class CustomerRepository :
        RepositoryBase<CustomerModel, HotelDBContext, int>,
        ICustomerRepository
    {
        public CustomerRepository(HotelDBContext dbContext)
        : base(dbContext)
        { }

        public CustomerModel? GetCustomer(string name)
        {
            return Query.Where(x => x.CustomerName == name).FirstOrDefault();
        }
    }
}