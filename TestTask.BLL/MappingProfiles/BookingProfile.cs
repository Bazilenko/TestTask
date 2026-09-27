using AutoMapper;
using TestTask.BLL.DTOs.Booking;
using TestTask.BLL.Extensions;
using TestTask.DAL.Entities;

namespace TestTask.BLL.MappingProfiles;
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<CreateBookingDto, Booking>()
            .IgnoreBaseAuditFields()
            .ForMember(dest => dest.EndTime, opt => opt.Ignore()) 
            .ForMember(dest => dest.HourlyRate, opt => opt.Ignore()) 
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) 

            .ForMember(dest => dest.Room, opt => opt.Ignore())
            .ForMember(dest => dest.BookingServices, opt => opt.Ignore());

        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.Services, opt => 
                opt.MapFrom(src => src.BookingServices.Select(bs => bs.Service)));
    }
}