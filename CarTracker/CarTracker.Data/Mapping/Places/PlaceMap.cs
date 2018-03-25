using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Places;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Places
{

    public class PlaceMap : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {

            builder.ToTable("PLACE").HasKey(p => p.PlaceId);

            builder.Property(p => p.PlaceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

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
    
            builder.AddTrackedEntityProperties();
        }
    }
}
