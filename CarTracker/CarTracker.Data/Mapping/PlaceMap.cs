using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
   

    public class PlaceMap : TrackedMap<Place>
    {
        public override void Configure(EntityTypeBuilder<Place> builder)
        {
            base.Configure(builder);

            builder.ToTable("PLACE").HasKey(p => p.PlaceId);

            builder.Property(p => p.PlaceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.GooglePlaceId)
                .HasColumnName("GOOGLE_PLACE_ID")
                .HasMaxLength(250);

            builder.Property(p => p.Name)
                .HasColumnName("NAME")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(p => p.Latitude)
                .HasColumnName("LATITUDE")
                .IsRequired();

            builder.Property(p => p.Longitude)
                .HasColumnName("LONGITUDE")
                .IsRequired();

            builder.Property(p => p.Active)
                .HasColumnName("ACTIVE")
                .IsRequired();
        }
    }
}
