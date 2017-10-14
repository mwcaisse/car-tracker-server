using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public abstract class TrackedMap<T> : IEntityTypeConfiguration<T> where T : class, ITrackedEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("MODIFIED_DATE")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
