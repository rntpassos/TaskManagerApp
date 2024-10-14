namespace TaskManagerApp.Domain.Interfaces;
public interface IBaseEntity
{
    long Id { get; set; }
    Guid Guid { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
    // bool IsActive { get; set; } // Adicionar se necessário
    string CreatedBy { get; set; }
    string UpdatedBy { get; set; }
}

