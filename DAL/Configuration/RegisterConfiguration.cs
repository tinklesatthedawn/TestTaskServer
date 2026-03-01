using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class RegisterConfiguration : IEntityTypeConfiguration<RegisterEntity>
    {
        public void Configure(EntityTypeBuilder<RegisterEntity> builder)
        {
            builder
                .HasOne<DeviceEntity>()
                .WithMany()
                .HasForeignKey(e => e.DeviceId);

            builder.ToTable("Registers");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder
                .Property(p => p.DeviceId)
                .HasColumnName("DeviceId")
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Description)
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(5000);

            builder
                .Property(p => p.EditingDate)
                .HasColumnName("EditingDate")
                .IsRequired();
        }
    }
}
