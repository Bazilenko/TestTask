using TestTask.DAL.Data;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class RoomServiceRepository : GenericRepository<RoomService>, IRoomServiceRepository
{
    public RoomServiceRepository(AppDbContext context) : base(context)
    {
    }
}