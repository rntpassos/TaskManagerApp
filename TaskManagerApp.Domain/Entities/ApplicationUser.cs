using Microsoft.AspNetCore.Identity;

namespace TaskManagerApp.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}