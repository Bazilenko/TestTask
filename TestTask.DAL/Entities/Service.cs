namespace TestTask.DAL.Entities;
public class Service : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    
    public ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();
    public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
        
}