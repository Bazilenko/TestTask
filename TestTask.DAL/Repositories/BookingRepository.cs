using TestTask.DAL.Data;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context)
    {
    }
}