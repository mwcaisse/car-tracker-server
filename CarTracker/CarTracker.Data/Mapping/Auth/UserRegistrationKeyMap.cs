using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Auth
{
    public class UserRegistrationKeyMap : TrackedMap<UserRegistrationKey>
    {
        public override void Configure(EntityTypeBuilder<UserRegistrationKey> builder)
        {
            base.Configure(builder);

            builder.ToTable("USER_REGISTRATION_KEY")
                .HasKey(k => k.UserRegistrationKeyId);

            builder.Property(k => k.UserRegistrationKeyId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(k => k.Key)
                .HasColumnName("KEY_VAL")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(k => k.UsesRemaining)
                .HasColumnName("USES_REMAINING")
                .IsRequired();

            builder.Property(k => k.Active)
                .HasColumnName("ACTIVE")
                .IsRequired();


        }
    }
}
