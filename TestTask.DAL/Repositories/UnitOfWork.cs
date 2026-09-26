using TestTask.DAL.Data;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{   
    private readonly AppDbContext _context;

    public IRoomRepository Rooms {get;}

    public IBookingRepository Bookings {get;}

    public IServiceRepository Services {get;}

    public UnitOfWork(AppDbContext context, IRoomRepository rooms, IServiceRepository services, IBookingRepository bookings)
    {
        _context = context;
        Rooms = rooms;
        Services = services;
        Bookings = bookings;
    }
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }
}
