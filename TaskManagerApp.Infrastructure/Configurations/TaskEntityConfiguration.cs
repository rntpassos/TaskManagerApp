using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TaskManagerApp.Domain.Entities;

namespace TaskManagerApp.Infrastructure.Configurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.Property(e => e.Id)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.TaskSequence");

            builder.Property(e => e.Guid)
                .IsRequired();

            builder.HasIndex(e => e.Guid)
                .IsUnique();

            builder.HasIndex(e => e.AssignedToUserId)
                .IsUnique();

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired();

            builder.OwnsOne(t => t.Location, loc =>
            {
                loc.Property(l => l.Address)
                   .HasMaxLength(250);
                loc.Property(l => l.City)
                   .HasMaxLength(100);
                loc.Property(l => l.State)
                   .HasMaxLength(20);
                loc.Property(l => l.Country)
                   .HasMaxLength(100);
                loc.Property(l => l.ZipCode)
                   .HasMaxLength(20);
            });
        }
    }
}
