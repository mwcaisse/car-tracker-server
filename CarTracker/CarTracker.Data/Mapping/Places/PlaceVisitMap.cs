using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities.Places;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Places
{
    public class PlaceVisitMap : IEntityTypeConfiguration<PlaceVisit>
    {
        public void Configure(EntityTypeBuilder<PlaceVisit> builder)
        {
            builder.ToTable("PLACE_VISIT")
                .HasKey(pv => pv.PlaceVisitId);

            builder.Property(pv => pv.PlaceVisitId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(pv => pv.Latitude)
                .HasColumnName("LATITUDE")
                .IsRequired();

            builder.Property(pv => pv.Longitude)
                .HasColumnName("LONGITUDE")
                .IsRequired();

            builder.Property(pv => pv.PlaceType)
                .HasColumnName("PLACE_TYPE")
                .IsRequired();

            builder.Property(pv => pv.VisitDate)
                .HasColumnName("VISIT_DATE");

            builder.Property(pv => pv.UserSelected)
                .HasColumnName("USER_SELECTED")
                .IsRequired();

            builder.Property(pv => pv.PlaceId)
                .HasColumnName("PLACE_ID")
                .IsRequired();

            builder.HasOne(pv => pv.Place)
                .WithMany(p => p.Visits)
                .HasForeignKey(pv => pv.PlaceId);
                
            builder.AddTrackedEntityProperties();
            builder.AddOwnedEntityProperties(u => u.PlaceVisits);
        }
    }
}
