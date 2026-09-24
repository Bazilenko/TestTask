using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTask.DAL.Configurations;
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Capacity)
            .IsRequired();

        builder.Property(x => x.HourlyRate)
            .IsRequired()
            .HasPrecision(18, 2);
        
    }
        
}
