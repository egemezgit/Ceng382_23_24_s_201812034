using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData
{
    public Room[] Room { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string? RoomId { get; set; }

    [JsonPropertyName("roomName")]
    public string? RoomName { get; set; }

    [JsonPropertyName("capacity")]
    public string? Capacity { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePath = "Data.json";
        string jsonString;

        try
        {
            jsonString = File.ReadAllText(jsonFilePath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File Not Found.");
            return;
        }

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

        if (roomData?.Room != null)
        {
            ReservationHandler reservationHandler = new ReservationHandler(7, roomData.Room.Length);

            // Add dummy reservations
            reservationHandler.AddReservation(0, 0, new Reservation
            {
                ReserverName = "John Doe",
                Room = new Room { RoomId = "001", RoomName = "A-101", Capacity = "30" },
                Date = DateTime.Today.AddDays(1), // Tomorrow
                Time = new TimeSpan(10, 0, 0) // 10:00 AM
            });

            reservationHandler.AddReservation(1, 1, new Reservation
            {
                ReserverName = "Jane Smith",
                Room = new Room { RoomId = "002", RoomName = "A-102", Capacity = "24" },
                Date = DateTime.Today.AddDays(2), // Day after tomorrow
                Time = new TimeSpan(14, 0, 0) // 2:00 PM
            });

            // Display schedule
            reservationHandler.DisplayWeeklySchedule();
        }
        else
        {
            Console.WriteLine("No room data found.");
        }
    }
}