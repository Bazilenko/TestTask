namespace TestTask.DAL.Entities;
public class Service : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public ICollection<RoomService> RoomServices { get; set; }
    public ICollection<BookingService> BookingServices { get; set; }
        
}