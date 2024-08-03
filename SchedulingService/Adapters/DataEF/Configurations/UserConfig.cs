using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataEF.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasDiscriminator<Roles>("Role")
            .HasValue<Admin>(Roles.Admin)
            .HasValue<Professional>(Roles.Professional);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.PasswordHash)
            .HasColumnType("text").IsRequired();
    }
}