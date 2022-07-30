namespace HotelBooking
{
    public interface IBookingManager
    {
        /** 
         * Return true if there is no booking for the given room on the date, 
         * otherwise false 
         */
        public bool IsRoomAvailable(int room, DateTime date);

        /**
         * Add a booking for the given guest in the given room on the given 
         * date. If the room is not available, throw a suitable Exception. 
         */
        public void AddBooking(string guest, int room, DateTime date);

        /**
        * Return a list of all the available room numbers for the given date 
        */
        public IEnumerable<int> GetAvailableRooms(DateTime date);
    }
}
