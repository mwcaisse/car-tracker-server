using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Places;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Places
{
    public class GooglePlaceMap : IEntityTypeConfiguration<GooglePlace>
    {
        public void Configure(EntityTypeBuilder<GooglePlace> builder)
        {
            builder.ToTable("GOOGLE_PLACE").HasKey(g => g.GooglePlaceId);

            builder.Property(g => g.GooglePlaceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(g => g.GoogleId)
                .HasColumnName("GOOGLE_ID")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(g => g.PlaceId)
                .HasColumnName("PLACE_ID")
                .IsRequired();

            builder.HasOne(g => g.Place)
                .WithMany(p => p.GooglePlaces)
                .HasForeignKey(g => g.PlaceId);

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
