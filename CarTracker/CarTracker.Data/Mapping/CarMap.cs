using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
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

            builder.Property(c => c.Mileage)
                .HasColumnName("MILEAGE");

            builder.Property(c => c.MileageLastUserSet)
                .HasColumnName("MILEAGE_LAST_USER_SET");

            builder.AddTrackedEntityProperties();
            builder.AddOwnedEntityProperties(u => u.Cars);
        }
    }
}
