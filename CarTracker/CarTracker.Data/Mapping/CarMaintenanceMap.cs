using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class CarMaintenanceMap : IEntityTypeConfiguration<CarMaintenance>
    {
        public void Configure(EntityTypeBuilder<CarMaintenance> builder)
        {
            builder.ToTable("CAR_MAINTENANCE").HasKey(c => c.CarMaintenanceId);

            builder.Property(c => c.CarMaintenanceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.CarId)
                .HasColumnName("CAR_ID")
                .IsRequired();

            builder.Property(c => c.Type)
                .HasColumnName("TYPE")
                .IsRequired();

            builder.Property(c => c.Notes)
                .HasColumnName("NOTES")
                .HasMaxLength(2000);

            builder.Property(c => c.Date)
                .HasColumnName("DATE")
                .IsRequired();

            builder.Property(c => c.Mileage)
                .HasColumnName("MILEAGE")
                .IsRequired();

            builder.HasOne(c => c.Car)
                .WithMany(c => c.CarMaintenances)
                .HasForeignKey(c => c.CarId);

            builder.AddTrackedEntityProperties();
            builder.AddActiveEntityProperties();
        }
    }
}
