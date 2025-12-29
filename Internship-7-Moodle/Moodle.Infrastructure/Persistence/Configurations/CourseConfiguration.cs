

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Entities;

namespace Moodle.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(c => c.Professor)
                .WithMany(u => u.CoursesTaught)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
