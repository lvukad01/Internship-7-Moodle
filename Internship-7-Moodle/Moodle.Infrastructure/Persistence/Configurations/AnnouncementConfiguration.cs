using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moodle.Domain.Entities;


namespace Moodle.Infrastructure.Persistence.Configurations
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("Announcements");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.HasOne(a => a.Course)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CourseId);

            builder.HasOne(a => a.Professor)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
