using System;

public class ReservationHandler
{
    private Reservation?[,] schedule;

    public ReservationHandler(int daysInWeek, int roomsCount)
    {
        schedule = new Reservation?[daysInWeek, roomsCount]; // Nullable Reservation
    }

    public void AddReservation(int dayIndex, int roomIndex, Reservation reservation)
    {
        // Calculate the hour of the day for the reservation
        int hourOfDay = reservation.Time.Hours;

        // Adjust the hour index to match the schedule array
        hourOfDay -= 9; // Assuming the schedule starts from 9:00 AM

        // Add the reservation to the schedule
        schedule[dayIndex, hourOfDay] = reservation;
    }

    public void DeleteReservation(int dayIndex, int roomIndex)
    {
        schedule[dayIndex, roomIndex] = null;
    }

    public void DisplayWeeklySchedule()
    {
        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        Console.WriteLine("This week's schedule:");

        // Display schedule header
        Console.WriteLine("+-----------+-------------+-------------+-------------+-------------+-------------+-------------+------------+");

        // Display day names
        Console.Write("| Time      ");
        foreach (var day in daysOfWeek)
        {
            Console.Write($"|   {day,-10}");
        }
        Console.WriteLine("|");

        // Display separator line
        Console.WriteLine("+-----------+-------------+-------------+-------------+-------------+-------------+-------------+------------+");

        // Display schedule by time slots
        for (int hour = 9; hour <= 17; hour++) // Assuming time slots from 9:00 AM to 5:00 PM
        {
            for (int minute = 0; minute < 60; minute += 30) // Time slots every 30 minutes
            {
                TimeSpan timeSlot = new TimeSpan(hour, minute, 0);
                Console.WriteLine("+-----------+-------------+-------------+-------------+-------------+-------------+-------------+------------+");

                // Display time slot
                Console.Write($"|{timeSlot.ToString("hh':'mm"),-10}");

                // Display room reservation for each day
                for (int dayIndex = 0; dayIndex < schedule.GetLength(0); dayIndex++)
                {
                    var reservation = schedule[dayIndex, hour - 9];

                    if (reservation != null && reservation.Time == timeSlot)
                    {
                        string roomName = reservation.Room?.RoomName ?? string.Empty;
                        Console.Write($" |    {roomName,-8}");
                    }
                    else
                    {
                        Console.Write(" |            "); // Empty cell
                    }
                }
                Console.WriteLine("|");
            }
        }
        Console.WriteLine("+-----------+-------------+-------------+-------------+-------------+-------------+-------------+------------+");
    }
}