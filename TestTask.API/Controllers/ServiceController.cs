using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.DTOs.Service;
using TestTask.BLL.Interfaces;

namespace TestTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceManagementService _serviceService;

    public ServiceController(IServiceManagementService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetAllServices(CancellationToken ct)
    {
        var services = await _serviceService.GetAllServicesAsync(ct);
        return Ok(services);
    }

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceDto request, CancellationToken ct)
    {
        var serviceId = await _serviceService.CreateServiceAsync(request, ct);
        return CreatedAtAction(nameof(GetAllServices), new { id = serviceId }, new { id = serviceId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceDto request, CancellationToken ct)
    {
        await _serviceService.UpdateServiceAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteService(int id, CancellationToken ct)
    {
        await _serviceService.DeleteServiceAsync(id, ct);
        return NoContent();
    }
}