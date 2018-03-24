using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class UserPlaceMap : IEntityTypeConfiguration<UserPlace>
    {
        public void Configure(EntityTypeBuilder<UserPlace> builder)
        {
            builder.ToTable("USER_PLACE")
                .HasKey(p => p.UserPlaceId);

            builder.Property(p => p.UserPlaceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Latitude)
                .HasColumnName("LATITUDE")
                .IsRequired();

            builder.Property(p => p.Longitude)
                .HasColumnName("LONGITUDE")
                .IsRequired();

            builder.AddTrackedEntityProperties();
            builder.AddOwnedEntityProperties(u => u.UserPlaces);
            builder.AddActiveEntityProperties();
        }
    }
}
