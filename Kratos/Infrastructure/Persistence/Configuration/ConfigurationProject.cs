using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ConfigurationProject : IEntityTypeConfiguration<Project>
{

    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id)
            .HasName("PK_PROJECTS");

        builder.Property(c => c.Id)
           .HasColumnName("ProjectId")
           .HasColumnOrder(1)
           .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnOrder(2)
            .IsRequired(true)
            .HasColumnType("VARCHAR(50)");

        builder.Property(x => x.CreatedAt)
            .HasColumnOrder(3)
            .IsRequired(true)
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(x => x.AlteredAt)
            .HasColumnOrder(4)
            .IsRequired(true)
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);


        builder.HasIndex(c => c.Id)
            .HasDatabaseName("IX_PROJECT_ID")
            .IsUnique();
    }
}
