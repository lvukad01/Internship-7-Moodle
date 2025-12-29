

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Moodle.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User> //mapiranje entiteta User na bazu podataka, kljucevi, ogranicenja
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired();
        }
    }
}
