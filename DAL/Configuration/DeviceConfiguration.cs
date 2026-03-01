using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Domain.Entities;

namespace DAL.Configuration
{
    internal class DeviceConfiguration : IEntityTypeConfiguration<DeviceEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceEntity> builder)
        {
            builder.ToTable("Devices");

            builder
                .HasOne<InterfaceEntity>()
                .WithMany()
                .HasForeignKey(e => e.InterfaceId);

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder
                .Property(p => p.InterfaceId)
                .HasColumnName("InterfaceId")
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Description)
                .HasColumnName ("Description")
                .IsRequired()
                .HasMaxLength(5000);

            builder
                .Property(p => p.IsEnabled)
                .HasColumnName("IsEnabled")
                .HasDefaultValue(true)
                .IsRequired();

            builder
                .Property(p => p.EditingDate)
                .HasColumnName("EditingDate")
                .IsRequired();

            builder
                .Property(p => p.Figure)
                .HasColumnName("Figure")
                .HasConversion(
                p => p.ToString(), 
                p => (DeviceEntity.FigureType)Enum.Parse(typeof(DeviceEntity.FigureType), p))
                .IsRequired();
           

            builder
                .Property(p => p.Size)
                .HasColumnName("Size")
                .IsRequired();

            builder
                .Property(p => p.PosX)
                .HasColumnName("PosX")
                .IsRequired();

            builder
                .Property(p => p.PosY)
                .HasColumnName("PosY")
                .IsRequired();

            builder
                .Property(p => p.Color)
                .HasColumnName("Color")
                .HasDefaultValue(Color.DarkGray)
                .HasConversion(e => e.ToArgb().ToString(), e => Color.FromArgb(Int32.Parse(e)))
                .IsRequired();
        }
    }
}
