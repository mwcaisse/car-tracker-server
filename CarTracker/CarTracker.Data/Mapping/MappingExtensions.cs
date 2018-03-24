using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public static class MappingExtensions
    {

        public static void AddTrackedEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : class, ITrackedEntity
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

        public static void AddOwnedEntityProperties<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<User, IEnumerable<T>>> navigationExpression ) where T : class, IOwnedEntity
        {
            builder.Property(e => e.OwnerId)
                .HasColumnName("OWNER_ID")
                .IsRequired();

            builder.HasOne(e => e.Owner)
                .WithMany(navigationExpression)
                .HasForeignKey(e => e.OwnerId);
        }

        public static void AddOwnedEntityProperties<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<User, T>> navigationExpression) where T : class, IOwnedEntity
        {
            builder.Property(e => e.OwnerId)
                .HasColumnName("OWNER_ID")
                .IsRequired();

            builder.HasOne(e => e.Owner)
                .WithOne(navigationExpression)
                .IsRequired();
        }

        public static void AddActiveEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : class, IActiveEntity
        {
            builder.Property(e => e.Active)
                .HasColumnName("ACTIVE")
                .IsRequired();
        }

    }
}
