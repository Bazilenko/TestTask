namespace TestTask.DAL.Entities;
public class BookingService
{
    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public decimal Price { get; set; }

    public Booking Booking { get; set; }

    public Service Service { get; set; }
}
