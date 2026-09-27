using TestTask.BLL.DTOs.Service;

namespace TestTask.BLL.Interfaces;

public interface IServiceManagementService
{
    Task<int> CreateServiceAsync(CreateServiceDto dto, CancellationToken ct);
    Task UpdateServiceAsync(int id, UpdateServiceDto dto, CancellationToken ct);
    Task DeleteServiceAsync(int id, CancellationToken ct);
    Task<List<ServiceDto>> GetAllServicesAsync(CancellationToken ct);
}