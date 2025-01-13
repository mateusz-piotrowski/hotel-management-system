using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace hotel_management_system
{
    public class RoomType
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }
        public List<string> Features { get; set; }
    }

    public class Room
    {
        public string RoomType { get; set; }
        public string RoomId { get; set; }
    }

    public class Hotel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<Room> Rooms { get; set; }
    }

    public class Booking
    {
        public string HotelId { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public string RoomType { get; set; }
        public string RoomRate { get; set; }
    }
    internal class HotelManagementSystem
    {
        private List<Hotel> Hotels { get; set; }
        private List<Booking> Bookings { get; set; }

        // Constructor now accepts deserialized data directly
        public HotelManagementSystem(string hotelsFile, string bookingsFile)
        {
            Hotels = JsonConvert.DeserializeObject<List<Hotel>>(File.ReadAllText(hotelsFile));
            Bookings = JsonConvert.DeserializeObject<List<Booking>>(File.ReadAllText(bookingsFile));
        }

        public int CheckAvailability(string hotelId, string dateRange, string roomType)
        {
            var hotel = Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            {
                Console.WriteLine($"Hotel with ID {hotelId} not found.");
                return 0;
            }

            var roomTypeObj = hotel.RoomTypes.FirstOrDefault(rt => rt.Code == roomType);
            if (roomTypeObj == null)
            {
                Console.WriteLine($"Room type {roomType} not found in hotel {hotelId}.");
                return 0;
            }

            // Parse the date range
            var dates = dateRange.Split('-');
            var startDate = DateTime.ParseExact(dates[0], "yyyyMMdd", null);
            DateTime endDate = startDate;
            if (dates.Length > 1)
            {
                endDate = DateTime.ParseExact(dates[1], "yyyyMMdd", null);
            }

            // Count the number of rooms of the requested type
            var totalRooms = hotel.Rooms.Count(r => r.RoomType == roomType);
            var bookedRooms = Bookings.Count(b =>
                b.HotelId == hotelId &&
                b.RoomType == roomType &&
                DateTime.ParseExact(b.Arrival, "yyyyMMdd", null) <= endDate &&
                DateTime.ParseExact(b.Departure, "yyyyMMdd", null) >= startDate);

            return totalRooms - bookedRooms;
        }

        public List<string> SearchAvailability(string hotelId, int daysAhead, string roomType)
        {
            var hotel = Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            {
                Console.WriteLine($"Hotel with ID {hotelId} not found.");
                return new List<string>();
            }

            var roomTypeObj = hotel.RoomTypes.FirstOrDefault(rt => rt.Code == roomType);
            if (roomTypeObj == null)
            {
                Console.WriteLine($"Room type {roomType} not found in hotel {hotelId}.");
                return new List<string>();
            }

            var availablePeriods = new List<string>();
            DateTime today = DateTime.Now;

            for (int i = 0; i < daysAhead; i++)
            {
                DateTime checkDate = today.AddDays(i);
                int availableRooms = CheckAvailability(hotelId, checkDate.ToString("yyyyMMdd"), roomType);

                if (availableRooms > 0)
                {
                    availablePeriods.Add($"({hotelId}, {checkDate:yyyyMMdd}-{checkDate.AddDays(1):yyyyMMdd}, {availableRooms})");
                }
            }

            return availablePeriods;
        }

    }
}
