using AutoMapper;
using TestTask.BLL.Exceptions;
using TestTask.BLL.Interfaces;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;
using TestTask.BLL.DTOs.Service;

namespace TestTask.BLL.Services;

public class ServiceManagementService : IServiceManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceManagementService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateServiceAsync(CreateServiceDto dto, CancellationToken ct)
    {
        var service = _mapper.Map<Service>(dto);

        await _unitOfWork.Services.AddAsync(service, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return service.Id;
    }

    public async Task UpdateServiceAsync(int id, UpdateServiceDto dto, CancellationToken ct)
    {
        var service = await _unitOfWork.Services.GetById(id, ct);
        
        if (service == null)
            throw new NotFoundException($"Service with ID {id} not found.");

        _mapper.Map(dto, service);

        _unitOfWork.Services.Update(service);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task DeleteServiceAsync(int id, CancellationToken ct)
    {
        var service = await _unitOfWork.Services.GetById(id, ct);
        
        if (service == null)
            throw new NotFoundException($"Service with ID {id} not found.");

        _unitOfWork.Services.Delete(service);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<List<ServiceDto>> GetAllServicesAsync(CancellationToken ct)
    {
        var services = await _unitOfWork.Services.GetAllAsync(s => true, ct);
        
        return _mapper.Map<List<ServiceDto>>(services);
    }
}