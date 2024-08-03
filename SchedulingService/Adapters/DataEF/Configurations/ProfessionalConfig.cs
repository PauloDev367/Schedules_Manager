using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEF.Configurations;

public class ProfessionalConfig : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.HasMany(p => p.DaysOfWork)
            .WithOne(d => d.Professional)
            .HasForeignKey(d => d.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Schedules)
            .WithOne(s => s.Professional)
            .HasForeignKey(s => s.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}