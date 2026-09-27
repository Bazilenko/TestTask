namespace TestTask.BLL.DTOs.Room;

public record RoomDto(
    int Id,
    string Name,
    int Capacity,
    decimal HourlyRate,
    IReadOnlyCollection<int> ServiceIds);