using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POIT2025.Entities;

namespace POIT2025.Data;

// Add-migration [MigrationName]
// update-database
public class ScheduleContext : IdentityDbContext<UniversityUser>
{
    public DbSet<Student> Students { get; set; }

    public DbSet<Group> Groups { get; set; }

    public ScheduleContext(DbContextOptions<ScheduleContext> options)
            : base(options)
    {
    }
}
