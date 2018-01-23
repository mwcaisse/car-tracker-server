using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Logging
{
    public class ServerLogMap : TrackedMap<ServerLog>
    {
        public override void Configure(EntityTypeBuilder<ServerLog> builder)
        {
            base.Configure(builder);

            builder.ToTable("SERVER_LOG")
                .HasKey(s => s.ServerLogId);

            builder.Property(s => s.ServerLogId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.RequestUuid)
                .HasColumnName("REQUEST_UUID")
                .HasMaxLength(64);

            builder.Property(s => s.Type)
                .HasColumnName("TYPE");

            builder.Property(s => s.Message)
                .HasColumnName("MESSAGE");

            builder.Property(s => s.ExceptionMessage)
                .HasColumnName("EXCEPTION_MESSAGE");

            builder.Property(s => s.StackTrace)
                .HasColumnName("STACK_TRACE");

        }
    }
}
