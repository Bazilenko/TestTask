using TestTask.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TestTask.DAL.Configurations;
public class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
{
    public void Configure(EntityTypeBuilder<RoomService> builder)
    {
        builder.HasKey(x => new
        {
            x.RoomId,
            x.ServiceId
        });

        builder.HasOne(x => x.Room)
            .WithMany(x => x.RoomServices)
            .HasForeignKey(x => x.RoomId);

        builder.HasOne(x => x.Service)
            .WithMany(x => x.RoomServices)
            .HasForeignKey(x => x.ServiceId);
    }
}