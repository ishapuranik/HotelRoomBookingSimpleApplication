namespace HotelRooms.Persistence.Models
{
    public class HotelRoomsModel
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? BookedDate { get; set; }
    }
}
