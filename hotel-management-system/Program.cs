using System;
using System.Linq;

namespace hotel_management_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hotelManagement = new HotelManagementSystem("hotels.json", "bookings.json");

            Console.WriteLine("Welcome in the Bellagio Hotel Management System!\n");

            while (true)
            {
                Console.WriteLine("Enter a command (Availability/Search) or a blank line to exit:");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                switch (true)
                {
                    case var _ when input.StartsWith("Availability"):
                        var partsAvailability = input.Substring(13, input.Length - 14).Split(',');
                        string hotelIdAvailability = partsAvailability[0].Trim();
                        string dateRange = partsAvailability[1].Trim();
                        string roomTypeAvailability = partsAvailability[2].Trim();

                        int availableRooms = hotelManagement.CheckAvailability(hotelIdAvailability, dateRange, roomTypeAvailability);
                        
                        if (availableRooms > 0)
                        {
                            Console.WriteLine($"Available rooms: {availableRooms}");
                        }

                        break;

                    case var _ when input.StartsWith("Search"):
                        var partsSearch = input.Substring(7, input.Length - 8).Split(',');
                        string hotelIdSearch = partsSearch[0].Trim();
                        int daysAhead = int.Parse(partsSearch[1].Trim());
                        string roomTypeSearch = partsSearch[2].Trim();

                        var availablePeriods = hotelManagement.SearchAvailability(hotelIdSearch, daysAhead, roomTypeSearch);

                        if (availablePeriods.Any())
                        {
                            Console.WriteLine("Available stays:");
                            Console.WriteLine(string.Join(",\n", availablePeriods));
                        }

                        break;

                    default:
                        // Handle any other cases if necessary
                        Console.WriteLine("Command not recognized!");
                        break;
                }

            }
        }
    }
}
