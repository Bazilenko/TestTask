namespace TestTask.DAL.Entities;

public abstract class BaseEntity
{
    public int Id {get; set;}
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}