using AutoMapper;
using TestTask.BLL.DTOs.Room;
using TestTask.BLL.Extensions;
using TestTask.DAL.Entities;

namespace TestTask.BLL.MappingProfiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<CreateRoomDto, Room>()
            .IgnoreBaseAuditFields() 
            .ForMember(dest => dest.RoomServices, opt => opt.Ignore());

        CreateMap<UpdateRoomDto, Room>()
            .IgnoreBaseAuditFields() 
            .ForMember(dest => dest.RoomServices, opt => opt.Ignore());

        CreateMap<Room, RoomDto>(); 
    }
}