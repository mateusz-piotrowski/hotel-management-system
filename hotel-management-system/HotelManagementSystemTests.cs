using System.Collections.Generic;
using Xunit;

namespace hotel_management_system
{
    public class HotelManagementSystemTests
    {
        private HotelManagementSystem CreateTestHotelManagement()
        {
            // Creating mock hotel data directly for testing
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    Id = "H1",
                    Name = "Hotel California",
                    RoomTypes = new List<RoomType>
                    {
                        new RoomType { Code = "SGL", Description = "Single Room", Amenities = new List<string> { "WiFi", "TV" }, Features = new List<string> { "Non-smoking" } },
                        new RoomType { Code = "DBL", Description = "Double Room", Amenities = new List<string> { "WiFi", "TV", "Minibar" }, Features = new List<string> { "Non-smoking", "Sea View" } }
                    },
                    Rooms = new List<Room>
                    {
                        new Room { RoomType = "SGL", RoomId = "101" },
                        new Room { RoomType = "SGL", RoomId = "102" },
                        new Room { RoomType = "DBL", RoomId = "201" },
                        new Room { RoomType = "DBL", RoomId = "202" }
                    }
                }
            };

            var bookings = new List<Booking>
            {
                new Booking { HotelId = "H1", Arrival = "20240901", Departure = "20240903", RoomType = "DBL", RoomRate = "Prepaid" },
                new Booking { HotelId = "H1", Arrival = "20240902", Departure = "20240905", RoomType = "SGL", RoomRate = "Standard" }
            };

            // Create HotelManagement instance
            return new HotelManagementSystem("hotels.json", "bookings.json");
        }

        [Fact]
        public void Test_CheckAvailability_NoBookings()
        {
            // Arrange
            var hotelManagement = CreateTestHotelManagement();

            // Act
            var availability = hotelManagement.CheckAvailability("H1", "20240901", "SGL");

            // Assert
            Assert.Equal(2, availability);  // Expected: 2 rooms available
        }

        [Fact]
        public void Test_CheckAvailability_WithOverlappingBooking()
        {
            // Arrange
            var hotelManagement = CreateTestHotelManagement();

            // Act
            var availability = hotelManagement.CheckAvailability("H1", "20240902", "SGL");

            // Assert
            Assert.Equal(1, availability);  // Expected: 1 room available as one room is booked
        }

        [Fact]
        public void Test_CheckAvailability_WithNoAvailability()
        {
            // Arrange
            var hotelManagement = CreateTestHotelManagement();

            // Act
            var availability = hotelManagement.CheckAvailability("H1", "20240901-20240903", "DBL");

            // Assert
            Assert.Equal(-1, availability);  // Expected: Negative availability due to overbooked room
        }

        [Fact]
        public void Test_SearchAvailability_WithAvailableRooms()
        {
            // Arrange
            var hotelManagement = CreateTestHotelManagement();

            // Act
            var availability = hotelManagement.SearchAvailability("H1", 30, "SGL");

            // Assert
            Assert.Contains("20240901-20240902", availability);  // Should be available for this range
        }

        [Fact]
        public void Test_SearchAvailability_WithNoAvailableRooms()
        {
            // Arrange
            var hotelManagement = CreateTestHotelManagement();

            // Act
            var availability = hotelManagement.SearchAvailability("H1", 30, "DBL");

            // Assert
            Assert.Empty(availability);  // No rooms should be available in this range
        }

        [Fact]
        public void Test_SearchAvailability_WithIntermittentAvailability()
        {
            // Arrange
            var hotelManagement = CreateTestHotelManagement();

            // Act
            var availability = hotelManagement.SearchAvailability("H1", 30, "SGL");

            // Assert
            Assert.Contains("20240904-20240905", availability);  // Should return available periods
        }
    }
}
