namespace TestTask.DAL.Entities;
public class Room : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public decimal HourlyRate { get; set; }

    public ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        
}