using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEF.Configurations;

public class DayOfWorkConfig : IEntityTypeConfiguration<DayOfWork>
{
    public void Configure(EntityTypeBuilder<DayOfWork> builder)
    {
        builder.ToTable("DaysOfWork");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasOne(d => d.Professional)
            .WithMany(p => p.DaysOfWork)
            .HasForeignKey(d => d.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}