using TaskManagerApp.Domain.Interfaces;

namespace TaskManagerApp.Domain.Entities;

public class Category : IBaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Guid Guid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

    public Category()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public Category(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
