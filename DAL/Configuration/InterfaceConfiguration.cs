using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace DAL.Configuration
{
    public class InterfaceConfiguration : IEntityTypeConfiguration<InterfaceEntity>
    {
        public void Configure(EntityTypeBuilder<InterfaceEntity> builder)
        {
            builder.ToTable("Interfaces");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

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
