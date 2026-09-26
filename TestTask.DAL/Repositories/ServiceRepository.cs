using TestTask.DAL.Data;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(AppDbContext context) : base(context)
    {
    }
}