using CarTracker.Common.Entities.Places;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping.Places
{
    public class UserPlaceMap : IEntityTypeConfiguration<UserPlace>
    {
        public void Configure(EntityTypeBuilder<UserPlace> builder)
        {
            builder.ToTable("USER_PLACE")
                .HasKey(p => p.UserPlaceId);

            builder.Property(p => p.UserPlaceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.PlaceId)
                .HasColumnName("PLACE_ID")
                .IsRequired();

            builder.HasOne(u => u.Place)
                .WithMany(p => p.UserPlaces)
                .HasForeignKey(g => g.PlaceId);

            builder.AddTrackedEntityProperties();
            builder.AddOwnedEntityProperties(u => u.UserPlaces);
            builder.AddActiveEntityProperties();
        }
    }
}
