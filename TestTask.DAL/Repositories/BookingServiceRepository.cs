using TestTask.DAL.Data;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;
public class BookingServiceRepository : GenericRepository<BookingService>, IBookingServiceRepository
{
    public BookingServiceRepository(AppDbContext context) : base(context)
    {
    }
}