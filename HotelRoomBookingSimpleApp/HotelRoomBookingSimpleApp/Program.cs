using HotelBooking;
using HotelBooking.Services;
using HotelRooms.Data;
using HotelRooms.Persistence;
using HotelRooms.Persistence.Persistence;
using HotelRooms.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelRoomBookingSimpleApp
{
    public class Program
    {
        //static Object _service = null;

        public static void Main(string[] args)
        {
            // Startup.cs finally :)
            Startup startup = new Startup();
            IServiceCollection services = new ServiceCollection();

            startup.ConfigureServices(services);
            startup.ConfigureRepositories(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Get Service and call method
            var service = serviceProvider.GetService<IBookingManager>();
            
            Console.WriteLine(service?.IsRoomAvailable(101, new DateTime(2022, 08, 02))); //outputs true

            service?.AddBooking("Surname", 101, new DateTime(2022, 08, 02));

            Console.WriteLine(service?.IsRoomAvailable(101, new DateTime(2022, 08, 02))); // outputs false 

            service?.AddBooking("Li", 101, new DateTime(2022, 08, 02)); // throws an exception

            /**************** Code using monitor and lock - couldn't complete it ********************************
            _service = service;

            Thread[] Threads = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                Threads[i] = new Thread(new ThreadStart(StartBooking));
                Threads[i].Name = "Child " + i;
            }
            foreach (Thread t in Threads)
                t.Start();

            Console.ReadLine();
              ******************************************************************************/
        }

        /* with DI in place at the moment I do not have idea how can I access the same object, 
         * so passed it in the argument.Not viable solution
         
        public static void StartBooking(IBookingManager bookingManager, string guestName, int roomNumber, DateTime date)
        {
            Monitor.Enter(_service);
            try
            {
                bookingManager.AddBooking("Surname", 101, new DateTime(2022, 08, 02));
            }
            finally
            {
                Monitor.Exit(_service);
            }
        }
        */
    }

    public class Startup
    {
        public Startup()
        {
            //ToDo: Configure appSettings.json file
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelDBContext>(option =>
            {
                option = new DbContextOptionsBuilder<HotelDBContext>()
                    .UseSqlServer("GetConnectionString(<ConnectionString>");
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IBookingManager, BookingManager>();

            services.AddSingleton<IBookingService, BookingService>();
        }

        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IHotelRoomsRepository, HotelRoomsRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}