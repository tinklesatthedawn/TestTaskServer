using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configuration
{
    internal class LogMessageConfiguration : IEntityTypeConfiguration<LogMessageEntity>
    {
        public void Configure(EntityTypeBuilder<LogMessageEntity> builder)
        {
            builder.ToTable("LogMessages");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder
                .Property(p => p.Source)
                .HasColumnName("Source")
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Type)
                .HasColumnName("MessageType")
                .HasConversion(
                p => p.ToString(),
                p => (LogMessageEntity.MessageType)Enum.Parse(typeof(LogMessageEntity.MessageType), p))
                .IsRequired();

            builder
                .Property(p => p.Title)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Body)
                .HasColumnName("MessageBody")
                .HasMaxLength(5000);

            
        }
    }
}
