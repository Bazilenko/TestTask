using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.DTOs.Booking;
using TestTask.BLL.DTOs.Room;
using TestTask.BLL.Interfaces;

namespace TestTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("available")]
    public async Task<ActionResult<List<RoomDto>>> GetAvailableRooms([FromQuery] SearchAvailableRoomsDto request, CancellationToken ct)
    {
        var availableRooms = await _bookingService.GetAvailableRoomsAsync(request, ct);
        return Ok(availableRooms);
    }

    [HttpPost]
    public async Task<ActionResult<BookingResultDto>> BookRoom([FromBody] CreateBookingDto request, CancellationToken ct)
    {
        var result = await _bookingService.BookRoomAsync(request, ct);
        return CreatedAtAction(nameof(BookRoom), new { id = result.BookingId }, result);
    }
}