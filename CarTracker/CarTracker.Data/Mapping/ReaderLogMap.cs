using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class ReaderLogMap : IEntityTypeConfiguration<ReaderLog>
    {

        public void Configure(EntityTypeBuilder<ReaderLog> builder)
        {

            builder.ToTable("READER_LOG").HasKey(l => l.ReaderLogId);

            builder.Property(l => l.ReaderLogId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(l => l.Type)
                .HasColumnName("TYPE");

            builder.Property(l => l.Message)
                .HasColumnName("MESSAGE")
                .HasMaxLength(10000);

            builder.Property(l => l.Date)
                .HasColumnName("DATE");

            builder.AddTrackedEntityProperties();
        }
    }
}
