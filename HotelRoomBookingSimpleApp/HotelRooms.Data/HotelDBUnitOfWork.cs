using HotelRooms.Persistence.UnitOfWork;

namespace HotelRooms.Persistence
{
    public class HotelDBUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HotelDBContext _fmdbContext;

        public HotelDBUnitOfWork(HotelDBContext fmdbContext)
        {
            _fmdbContext = fmdbContext;
        }

        public bool IsDisposed { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _fmdbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (IsDisposed) return;

            _fmdbContext.Dispose();

            GC.SuppressFinalize(this);

            IsDisposed = true;
        }
    }
}
