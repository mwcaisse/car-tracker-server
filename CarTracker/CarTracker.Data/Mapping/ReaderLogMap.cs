using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class ReaderLogMap : TrackedMap<ReaderLog>
    {

        public override void Configure(EntityTypeBuilder<ReaderLog> builder)
        {
            base.Configure(builder);

            builder.ToTable("READER_LOG").HasKey(l => l.ReaderLogId);

            builder.Property(l => l.ReaderLogId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(l => l.Type)
                .HasColumnName("TYPE")
                .HasMaxLength(50);

            builder.Property(l => l.Message)
                .HasColumnName("MESSAGE")
                .HasMaxLength(10000);

            builder.Property(l => l.Date)
                .HasColumnName("DATE");
        }
    }
}
