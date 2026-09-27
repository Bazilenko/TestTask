using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.DTOs.Room;
using TestTask.BLL.Interfaces;

namespace TestTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomManagementService _roomService;

    public RoomController(IRoomManagementService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto request, CancellationToken ct)
    {
        var roomId = await _roomService.CreateRoomAsync(request, ct);
        return CreatedAtAction(nameof(CreateRoom), new { id = roomId }, new { id = roomId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomDto request, CancellationToken ct)
    {
        await _roomService.UpdateRoomAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRoom(int id, CancellationToken ct)
    {
        await _roomService.DeleteRoomAsync(id, ct);
        return NoContent();
    }
}