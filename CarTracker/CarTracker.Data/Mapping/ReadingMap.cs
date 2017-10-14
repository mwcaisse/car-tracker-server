using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class ReadingMap : TrackedMap<Reading>
    {
        public override void Configure(EntityTypeBuilder<Reading> builder)
        {
            base.Configure(builder);

            builder.ToTable("READING")
                .HasKey(r => r.ReadingId);

            builder.Property(r => r.ReadingId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.ReadDate)
                .HasColumnName("READ_DATE")
                .IsRequired();

            builder.Property(r => r.TripId)
                .HasColumnName("TRIP_ID")
                .IsRequired();

            builder.Property(r => r.Latitude)
                .HasColumnName("LATITUDE")
                .IsRequired();

            builder.Property(r => r.Longitude)
                .HasColumnName("LONGITUDE")
                .IsRequired();

            builder.Property(r => r.AirIntakeTemperature)
                .HasColumnName("AIR_INTAKE_TEMP");

            builder.Property(r => r.AmbientAirTemperature)
                .HasColumnName("AMBIENT_AIR_TEMP");

            builder.Property(r => r.EngineCoolantTemperature)
                .HasColumnName("ENGINE_COOLANT_TEMP");

            builder.Property(r => r.OilTemperature)
                .HasColumnName("OIL_TEMP");

            builder.Property(r => r.EngineRpm)
                .HasColumnName("ENGINE_RPM");

            builder.Property(r => r.Speed)
                .HasColumnName("SPEED");

            builder.Property(r => r.MassAirFlow)
                .HasColumnName("MASS_AIR_FLOW");

            builder.Property(r => r.ReadDate)
                .HasColumnName("THROTTLE_POSITION");

            builder.Property(r => r.FuelType)
                .HasColumnName("FUEL_TYPE")
                .HasMaxLength(250);

            builder.Property(r => r.FuelLevel)
                .HasColumnName("FUEL_LEVEL");

            builder.HasOne(r => r.Trip)
                .WithMany(t => t.Readings)
                .IsRequired();
        }
    }
}
