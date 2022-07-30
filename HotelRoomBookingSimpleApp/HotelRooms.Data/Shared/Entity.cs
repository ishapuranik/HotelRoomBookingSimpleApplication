namespace HotelRooms.Persistence.Shared
{
    public abstract class Entity<T>
    {
        public T Id { get; internal protected set; }
    }
}
