namespace TestTask.BLL.DTOs.Booking;

public record CreateBookingDto(
    int RoomId,
    DateTimeOffset StartTime,
    int DurationHours,
    List<int>? ServiceIds = null 
);