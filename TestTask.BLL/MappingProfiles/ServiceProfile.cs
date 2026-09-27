using AutoMapper;
using TestTask.BLL.DTOs.Service;
using TestTask.BLL.Extensions;
using TestTask.DAL.Entities;

namespace TestTask.BLL.MappingProfiles;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceDto, Service>()
            .IgnoreBaseAuditFields()
            .ForMember(dest => dest.RoomServices, opt => opt.Ignore())
            .ForMember(dest => dest.BookingServices, opt => opt.Ignore());

        CreateMap<UpdateServiceDto, Service>()
            .IgnoreBaseAuditFields()
            .ForMember(dest => dest.RoomServices, opt => opt.Ignore())
            .ForMember(dest => dest.BookingServices, opt => opt.Ignore());

        CreateMap<Service, ServiceDto>();
    }
}