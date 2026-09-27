
using TestTask.BLL.DTOs.Service;

namespace TestTask.BLL.DTOs.Booking;

public record BookingDto(
    int Id, 
    int RoomId, 
    DateTimeOffset StartTime, 
    DateTimeOffset EndTime, 
    decimal TotalPrice,
    List<ServiceDto> Services
);