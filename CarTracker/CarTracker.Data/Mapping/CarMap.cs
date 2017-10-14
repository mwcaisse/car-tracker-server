﻿using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class CarMap : TrackedMap<Car>
    {
        public override void Configure(EntityTypeBuilder<Car> builder)
        {
            base.Configure(builder);

            builder.ToTable("CAR")
                .HasKey(c => c.CarId);

            builder.Property(c => c.CarId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Vin)
                .HasColumnName("VIN")
                .IsRequired()
                .HasMaxLength(17);

            builder.Property(c => c.Name)
                .HasColumnName("NAME")
                .HasMaxLength(250);

            builder.Property(c => c.OwnerId)
                .HasColumnName("OWNER_ID");

            builder.HasOne(c => c.Owner)
                .WithMany(u => u.Cars)
                .HasForeignKey(c => c.OwnerId);

            builder.Property(c => c.Mileage)
                .HasColumnName("MILEAGE");

            builder.Property(c => c.MileageLastUserSet)
                .HasColumnName("MILEAGE_LAST_USER_SET");
        }
    }
}
