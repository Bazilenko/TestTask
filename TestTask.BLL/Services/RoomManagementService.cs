using AutoMapper;
using TestTask.BLL.DTOs.Room;
using TestTask.BLL.Exceptions;
using TestTask.BLL.Interfaces;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.BLL.Services;

public class RoomManagementService : IRoomManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomManagementService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateRoomAsync(CreateRoomDto dto, CancellationToken ct)
    {
        var room = _mapper.Map<Room>(dto);

        if (dto.ServiceIds != null && dto.ServiceIds.Any())
        {
            foreach (var serviceId in dto.ServiceIds.Distinct()) 
            {
                room.RoomServices.Add(new RoomService { ServiceId = serviceId });
            }
        }

        await _unitOfWork.Rooms.AddAsync(room, ct); 
        await _unitOfWork.SaveChangesAsync(ct); 

        return room.Id;
    }

    public async Task UpdateRoomAsync(int roomId, UpdateRoomDto dto, CancellationToken ct)
    {
        var room = await _unitOfWork.Rooms.GetById(roomId, ct);
        
        if (room == null)
            throw new NotFoundException($"Room with ID {roomId} not found.");

        _mapper.Map(dto, room);

        if (dto.ServiceIds != null)
        {
            var newServiceIds = dto.ServiceIds.Distinct().ToList();
            
            var existingServices = await _unitOfWork.RoomServices.GetAllAsync(rs => rs.RoomId == roomId, ct);
            var existingServiceIds = existingServices.Select(es => es.ServiceId).ToList();

            var servicesToRemove = existingServices.Where(es => !newServiceIds.Contains(es.ServiceId)).ToList();
            foreach (var s in servicesToRemove)
            {
                _unitOfWork.RoomServices.HardDelete(s); 
            }

            var servicesToAdd = newServiceIds.Where(id => !existingServiceIds.Contains(id)).ToList();
            foreach (var id in servicesToAdd)
            {
                await _unitOfWork.RoomServices.AddAsync(new RoomService { RoomId = roomId, ServiceId = id }, ct);
            }
        }

        _unitOfWork.Rooms.Update(room);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task DeleteRoomAsync(int roomId, CancellationToken ct)
    {
        var room = await _unitOfWork.Rooms.GetById(roomId, ct);
        
        if (room == null)
            throw new NotFoundException($"Room with ID {roomId} not found.");

        _unitOfWork.Rooms.Delete(room); 
        await _unitOfWork.SaveChangesAsync(ct);
    }
}