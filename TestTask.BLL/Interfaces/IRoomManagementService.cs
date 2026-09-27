
using TestTask.BLL.DTOs.Room;

namespace TestTask.BLL.Interfaces;
public interface IRoomManagementService
{
    Task<int> CreateRoomAsync(CreateRoomDto dto, CancellationToken ct);
    Task UpdateRoomAsync(int roomId, UpdateRoomDto dto, CancellationToken ct);
    Task DeleteRoomAsync(int roomId, CancellationToken ct);
    
    // Task<List<RoomDto>> GetAvailableRoomsAsync(DateTimeOffset date, TimeSpan startTime, TimeSpan endTime, int capacity, CancellationToken ct);
}