namespace TestTask.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.DAL.Entities;

public class BookingServiceConfiguration : IEntityTypeConfiguration<BookingService>
{
    public void Configure(EntityTypeBuilder<BookingService> builder)
    {
        builder.HasKey(x => new
        {
            x.BookingId,
            x.ServiceId
        });

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne(x => x.Booking)
            .WithMany(x => x.BookingServices)
            .HasForeignKey(x => x.BookingId);

        builder.HasOne(x => x.Service)
            .WithMany(x => x.BookingServices)
            .HasForeignKey(x => x.ServiceId);
    }
}
