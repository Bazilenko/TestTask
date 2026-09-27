using TestTask.BLL.DTOs.Booking;
using TestTask.BLL.DTOs.Room;

namespace TestTask.BLL.Interfaces;
public interface IBookingService
{
    Task<List<RoomDto>> GetAvailableRoomsAsync(SearchAvailableRoomsDto request, CancellationToken ct);
    Task<BookingResultDto> BookRoomAsync(CreateBookingDto request, CancellationToken ct);
}