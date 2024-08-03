using DataEF.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataEF;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<DayOfWork> DayOfWorks { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new ProfessionalConfig());
        modelBuilder.ApplyConfiguration(new DayOfWorkConfig());
    }
}