namespace TestTask.BLL.DTOs.Booking;

public record SearchAvailableRoomsDto(
    DateTimeOffset StartTime, 
    DateTimeOffset EndTime, 
    int MinimumCapacity
);