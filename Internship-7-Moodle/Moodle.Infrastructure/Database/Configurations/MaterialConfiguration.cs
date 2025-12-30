

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Entities;

namespace Moodle.Infrastructure.Database.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired();

            builder.Property(m => m.Url)
                .IsRequired();

            builder.HasOne(m => m.Course)
                .WithMany(c => c.Materials)
                .HasForeignKey(m => m.CourseId);

            builder.HasOne(m => m.Professor)
                .WithMany(u => u.Materials)
                .HasForeignKey(m => m.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
