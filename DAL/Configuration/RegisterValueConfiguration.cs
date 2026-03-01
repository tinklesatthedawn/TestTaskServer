using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class RegisterValueConfiguration : IEntityTypeConfiguration<RegisterValueEntity>
    {
        public void Configure(EntityTypeBuilder<RegisterValueEntity> builder)
        {
            builder
                .HasOne<RegisterEntity>()
                .WithMany()
                .HasForeignKey(e => e.RegisterId);

            builder.ToTable("RegisterValues");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder
                .Property(p => p.RegisterId)
                .HasColumnName("RegisterId")
                .IsRequired();

            builder
                .Property(p => p.Timestamp)
                .HasColumnName("Timestamp")
                .IsRequired();
        }
    }
}
