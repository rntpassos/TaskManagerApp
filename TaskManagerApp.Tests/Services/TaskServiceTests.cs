using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerApp.Application.Interfaces;
using TaskManagerApp.Application.Services;
using TaskManagerApp.Domain.Entities;
using TaskManagerApp.Domain.Interfaces;
using TaskManagerApp.Tests.Data;

public class TaskServiceTests : IDisposable
{
    private readonly TestDbContext _context;
    private readonly Mock<IRepository<TaskEntity>> _mockRepository;
    private readonly Mock<ILogger<TaskService>> _mockLogger;
    private readonly ITaskService _taskService;

    public TaskServiceTests()
    {
        // Configurar a conexão com SQLite em memória
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("Filename=:memory:")
            .Options;

        // Criação do contexto
        _context = new TestDbContext(options);
        _context.Database.OpenConnection(); // Abre o banco de dados em memória
        _context.Database.EnsureCreated(); // Cria o esquema do banco de dados

        // Configurar o repositório com o contexto
        _mockRepository = new Mock<IRepository<TaskEntity>>();
        _mockLogger = new Mock<ILogger<TaskService>>();
        _taskService = new TaskService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task AddTaskAsync_ShouldAddTask_WhenTaskIsValid()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Description = "Test Task" };

        // Act
        await _taskService.AddTaskAsync(task);

        // Assert
        _mockRepository.Verify(r => r.AddAsync(task), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_Should_Return_Not_Found()
    {
        var nonExistentTask = new TaskEntity { 
            Id = 123,
            Description = "teste 123"
        };
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _taskService.UpdateTaskAsync(nonExistentTask)
        );
        Assert.Contains("Não encontrada Task com id 123.", exception.Message);
    }
    [Fact]
    public async Task UpdateTaskAsync_Is_Valid()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Description = "Test Task" };

        // Act
        await _taskService.AddTaskAsync(task);
    }

    [Fact]
    public async Task UpdateTaskAsync_Should_Call_UpdateAsync_Once()
    {
        // Arrange
        var task = new TaskEntity { Id = 1, Description = "Test Task" };

        // Configurar o mock para simular que a task existe no repositório
        _mockRepository.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

        // Act
        await _taskService.UpdateTaskAsync(task);

        // Assert
        // Verifica se o método UpdateAsync foi chamado uma vez com a entidade task
        _mockRepository.Verify(r => r.UpdateAsync(task), Times.Once);

        // Verifica se o método GetByIdAsync foi chamado uma vez para buscar a entidade
        _mockRepository.Verify(r => r.GetByIdAsync(task.Id), Times.Once);
    }


    public void Dispose()
    {
        _context.Database.EnsureDeleted(); // Remove o banco de dados após os testes
        _context.Dispose(); // Descarrega o contexto
    }
}
