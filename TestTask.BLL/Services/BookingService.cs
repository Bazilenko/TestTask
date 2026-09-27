using AutoMapper;
using TestTask.BLL.DTOs.Booking;
using TestTask.BLL.DTOs.Room;
using TestTask.BLL.Exceptions;
using TestTask.BLL.Interfaces;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.BLL.Services;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPriceCalculatorService _priceCalculator;

    public BookingService(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IPriceCalculatorService priceCalculator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceCalculator = priceCalculator;
    }

    public async Task<List<RoomDto>> GetAvailableRoomsAsync(SearchAvailableRoomsDto request, CancellationToken ct)
    {
    
        var overlappingBookings = await _unitOfWork.Bookings.GetAllAsync(b =>
            b.StartTime < request.EndTime &&
            b.EndTime > request.StartTime, ct);

        var busyRoomIds = overlappingBookings.Select(b => b.RoomId).Distinct().ToList();

        var availableRooms = await _unitOfWork.Rooms.GetAllAsync(r =>
            r.Capacity >= request.MinimumCapacity &&
            !busyRoomIds.Contains(r.Id), ct);

        return _mapper.Map<List<RoomDto>>(availableRooms);
    }

    public async Task<BookingResultDto> BookRoomAsync(CreateBookingDto request, CancellationToken ct)
    {
        var endTime = request.StartTime.AddHours(request.DurationHours);

        var room = await _unitOfWork.Rooms.GetById(request.RoomId, ct);
        if (room == null)
            throw new NotFoundException($"Зал з ID {request.RoomId} не знайдено.");

        var overlappingBookings = await _unitOfWork.Bookings.GetAllAsync(b =>
            b.RoomId == request.RoomId &&
            b.StartTime < endTime &&
            b.EndTime > request.StartTime, ct);

        if (overlappingBookings.Any())
            throw new ConflictException("This room is currently booked by someone else.");

        decimal roomPrice = _priceCalculator.CalculateRoomPrice(room.HourlyRate, request.StartTime, endTime);

        decimal servicesPrice = 0;
        
        var bookingServices = new List<DAL.Entities.BookingService>();

        if (request.ServiceIds != null && request.ServiceIds.Any())
        {
            var uniqueServiceIds = request.ServiceIds.Distinct().ToList();
            
            var existingServices = await _unitOfWork.Services.GetAllAsync(s => uniqueServiceIds.Contains(s.Id), ct);

            if (existingServices.Count != uniqueServiceIds.Count)
                throw new NotFoundException("Services are not found in system");

            var allowedRoomServices = await _unitOfWork.RoomServices.GetAllAsync(rs => rs.RoomId == request.RoomId, ct);
            var allowedServiceIds = allowedRoomServices.Select(rs => rs.ServiceId).ToList();

            foreach (var service in existingServices)
            {
                if (!allowedServiceIds.Contains(service.Id))
                    throw new BadRequestException($"Service '{service.Name}' is not for '{room.Name}'.");

                servicesPrice += service.Price;
                
                bookingServices.Add(new DAL.Entities.BookingService
                {
                    ServiceId = service.Id,
                    Price = service.Price
                });
            }
        }

        var totalPrice = roomPrice + servicesPrice;

        var booking = new Booking
        {
            RoomId = request.RoomId,
            StartTime = request.StartTime,
            EndTime = endTime,
            HourlyRate = room.HourlyRate,
            TotalPrice = totalPrice,
            BookingServices = bookingServices
        };

        await _unitOfWork.Bookings.AddAsync(booking, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return new BookingResultDto(booking.Id, totalPrice);
    }
}