namespace TestTask.DAL.Entities;
public class BookingService : BaseEntity
{
    public int BookingId { get; set; }
    public int ServiceId { get; set; }
    public decimal Price { get; set; }

    public Booking Booking { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
