using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Entities;

namespace TestTask.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingService> BookingServices => Set<BookingService>();
    public DbSet<RoomService> RoomServices => Set<RoomService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(AppDbContext).Assembly);

        AddSoftDeleteFilter<Room>(modelBuilder);
        AddSoftDeleteFilter<Service>(modelBuilder);
        AddSoftDeleteFilter<Booking>(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    private static void AddSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder) 
        where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>()
        .HasQueryFilter(x => !x.IsDeleted);
    }
        
}