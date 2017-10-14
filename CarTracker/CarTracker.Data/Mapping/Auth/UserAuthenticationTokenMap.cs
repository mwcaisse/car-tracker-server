using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Auth
{
    public class UserAuthenticationTokenMap :TrackedMap<UserAuthenticationToken>
    {
        public override void Configure(EntityTypeBuilder<UserAuthenticationToken> builder)
        {
            base.Configure(builder);

            builder.ToTable("USER_AUTHENTICATION_TOKEN")
                .HasKey(u => u.UserAuthenticationTokenId);

            builder.Property(u => u.UserAuthenticationTokenId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Property(u => u.Token)
                .HasColumnName("TOKEN")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(u => u.DeviceUuid)
                .HasColumnName("DEVICE_UUID")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(u => u.Active)
                .HasColumnName("ACTIVE")
                .IsRequired();

            builder.Property(u => u.LastLogin)
                .HasColumnName("LAST_LOGIN");

            builder.Property(u => u.LastLoginAddress)
                .HasColumnName("LAST_LOGIN_ADDRESS")
                .HasMaxLength(250);

            builder.Property(u => u.ExpirationDate)
                .HasColumnName("EXPIRATION_DATE");

            builder.HasOne(t => t.User)
                .WithMany(u => u.UserAuthenticationTokens)
                .HasForeignKey(t => t.UserId);
        }
    }
}
