namespace TestTask.BLL.DTOs.Room;

public record RoomDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int Capacity { get; init; }
}