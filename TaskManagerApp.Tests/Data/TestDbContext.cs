namespace TaskManagerApp.Tests.Data;

using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Domain.Entities;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da TaskEntity
        modelBuilder.Entity<TaskEntity>()
            .OwnsOne(t => t.Location); // Configura Location como um Value Object
    }
}
