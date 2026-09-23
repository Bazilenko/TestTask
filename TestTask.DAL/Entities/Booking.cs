namespace TestTask.DAL.Entities;

public class Booking : BaseEntity
{
    public int RoomId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal TotalPrice { get; set; }
    
    public Room Room { get; set; }
    public ICollection<BookingService> BookingServices { get; set; }
}