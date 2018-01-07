using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Auth
{
    public class UserRegistrationKeyUseMap : TrackedMap<UserRegistrationKeyUse>
    {
        public override void Configure(EntityTypeBuilder<UserRegistrationKeyUse> builder)
        {
            base.Configure(builder);

            builder.ToTable("USER_REGISTRATION_KEY_USE")
                .HasKey(u => u.UserRegistrationKeyUseId);

            builder.Property(u => u.UserRegistrationKeyUseId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.UserRegistrationKeyId)
                .HasColumnName("KEY_ID")
                .IsRequired();

            builder.Property(u => u.UserId)
                .HasColumnName("USER_ID");

            builder.HasOne(u => u.UserRegistrationKey)
                .WithMany(k => k.UserRegistrationKeyUses)
                .HasForeignKey(u => u.UserRegistrationKeyId);

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRegistrationKeyUses)
                .HasForeignKey(ur => ur.UserId);
        }
    }
}
