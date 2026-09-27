namespace TestTask.BLL.DTOs.Room;

public record UpdateRoomDto(
    string Name,
    int Capacity,
    decimal HourlyRate,
    IReadOnlyCollection<int> ServiceIds);