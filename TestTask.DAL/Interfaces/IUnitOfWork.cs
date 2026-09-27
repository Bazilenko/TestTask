namespace TestTask.DAL.Interfaces;
public interface IUnitOfWork
{
    IRoomRepository Rooms { get; }

    IBookingRepository Bookings { get; }

    IServiceRepository Services { get; }

    IRoomServiceRepository RoomServices {get; }

    IBookingServiceRepository BookingServices {get; }
    Task SaveChangesAsync(CancellationToken ct);
}