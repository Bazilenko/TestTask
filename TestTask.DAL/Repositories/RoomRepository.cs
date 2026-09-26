using TestTask.DAL.Data;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    public RoomRepository(AppDbContext context) : base(context)
    {
    }
}