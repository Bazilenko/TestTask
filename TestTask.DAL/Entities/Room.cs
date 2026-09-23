namespace TestTask.DAL.Entities;
public class Room : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal HourlyRate { get; set; }

    public ICollection<RoomService> RoomServices { get; set; }
    public ICollection<Booking> Bookings { get; set; }
        
}