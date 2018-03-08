using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Logging
{
    public class RequestLogMap : IEntityTypeConfiguration<RequestLog>
    {

        public void Configure(EntityTypeBuilder<RequestLog> builder)
        {
            builder.ToTable("REQUEST_LOG")
                .HasKey(r => r.RequestLogId);

            builder.Property(r => r.RequestLogId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.RequestUuid)
                .HasColumnName("REQUEST_UUID")
                .HasMaxLength(64);

            builder.Property(r => r.Type)
                .HasColumnName("TYPE");

            builder.Property(r => r.ClientAddress)
                .HasColumnName("CLIENT_ADDRESS")
                .HasMaxLength(255);

            builder.Property(r => r.RequestUrl)
                .HasColumnName("REQUEST_URL")
                .HasMaxLength(255);

            builder.Property(r => r.RequestMethod)
                .HasColumnName("REQUEST_METHOD")
                .HasMaxLength(50);

            builder.Property(r => r.RequestHeaders)
                .HasColumnName("REQUEST_HEADERS");

            builder.Property(r => r.RequestBody)
                .HasColumnName("REQUEST_BODY");

            builder.Property(r => r.ResponseStatus)
                .HasColumnName("RESPONSE_STATUS")
                .HasMaxLength(10);

            builder.Property(r => r.ResponseHeaders)
                .HasColumnName("RESPONSE_HEADERS");

            builder.Property(r => r.ResponseBody)
                .HasColumnName("RESPONSE_BODY");

            builder.Property(r => r.UserId)
                .HasColumnName("USER_ID");

            builder.HasOne(r => r.User)
                .WithMany(u => u.RequestLogs)
                .HasForeignKey(r => r.UserId);

            builder.AddTrackedEntityProperties();
        }

    }
}
