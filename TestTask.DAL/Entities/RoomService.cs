namespace TestTask.DAL.Entities;
public class RoomService : BaseEntity
{
    public int RoomId { get; set; }
    public int ServiceId { get; set; }
    
    public Room Room { get; set; } = null!;
    public Service Service { get; set; } = null!;
}