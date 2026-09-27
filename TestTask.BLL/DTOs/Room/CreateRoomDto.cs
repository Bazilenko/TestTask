namespace TestTask.BLL.DTOs.Room;

public record CreateRoomDto(
    string Name,
    int Capacity,
    decimal HourlyRate,
    IReadOnlyCollection<int> ServiceIds);