using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace TestTask.DAL.Configurations;
public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}