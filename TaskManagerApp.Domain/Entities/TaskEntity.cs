using TaskManagerApp.Domain.Interfaces;
using TaskManagerApp.Domain.ValueObjects;

namespace TaskManagerApp.Domain.Entities;

public class TaskEntity : IBaseEntity
{
    public long Id { get; set; }
    public Guid Guid { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Location? Location { get; set; }
    public long AssignedToUserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

    public TaskEntity()
    {
        Guid = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TaskEntity(string description, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        if (endDate < startDate)
            throw new ArgumentException("End date cannot be earlier than start date.");

        Description = description;
        StartDate = startDate;
        EndDate = endDate;

        Guid = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}