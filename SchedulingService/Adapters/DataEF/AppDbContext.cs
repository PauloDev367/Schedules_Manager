using DataEF.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataEF;

public class AppDbContext : DbContext
{
    public User Users { get; set; }
    public Schedule Schedules { get; set; }
    public DayOfWork DayOfWorks { get; set; }
    
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