using System;

public class Reservation
{
    public string? ReserverName { get; set; }
    public Room? Room { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }

    public Reservation()
    {
        // Initialize non-nullable properties with default values
        Date = DateTime.MinValue;
        Time = TimeSpan.Zero;
    }
}