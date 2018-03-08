using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class CarSupportedCommandsMap : IEntityTypeConfiguration<CarSupportedCommands>
    {

        public void Configure(EntityTypeBuilder<CarSupportedCommands> builder)
        {

            builder.ToTable("CAR_SUPPORTED_COMMANDS")
                .HasKey(c => c.CarSupportedCommandsId);

            builder.Property(c => c.CarSupportedCommandsId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.CarId)
                .HasColumnName("CAR_ID")
                .IsRequired();

            builder.HasOne(c => c.Car)
                .WithOne(c => c.CarSupportedCommands)
                .IsRequired();

            builder.Property(c => c.Pids0120Bitmask)
                .HasColumnName("PIDS_01_20_BITMASK");

            builder.Property(c => c.Pids2140Bitmask)
                .HasColumnName("PIDS_21_40_BITMASK");

            builder.Property(c => c.Pids4160Bitmask)
                .HasColumnName("PIDS_41_60_BITMASK");

            builder.Property(c => c.Pids6180Bitmask)
                .HasColumnName("PIDS_61_80_BITMASK");

            builder.Property(c => c.Pids81A0Bitmask)
                .HasColumnName("PIDS_81_A0_BITMASK");

            builder.AddTrackedEntityProperties();
        }

    }
}
