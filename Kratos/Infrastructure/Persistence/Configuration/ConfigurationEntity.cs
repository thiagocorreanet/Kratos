

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ConfigurationEntity : IEntityTypeConfiguration<Entity>
{
    private const string TableName = "Entities";
    private const string PrimaryKeyName = "PK_ENTITIES";
    private const string ForeignKeyName = "FK_ENTITY_PROJECT";
    
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id)
            .HasName(PrimaryKeyName);

         builder.Property(c => c.Id)
            .HasColumnName("EntityId")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd();

         builder.Property(c => c.Name)
           .HasColumnName("NameEntite")
           .HasColumnOrder(2)
           .IsRequired()
           .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(c => c.AlteredAt)
            .HasColumnName("AlteredAt")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(c => c.ProjectId)
            .HasColumnName("ProjectId")
            .HasColumnOrder(5)
            .IsRequired(true)
            .HasColumnType("INT");

        builder.HasIndex(c => c.Id)
            .HasDatabaseName("IX_ENTITIE_ID")
            .IsUnique();

        builder.HasOne(b => b.ProjectRel)
            .WithMany(c => c.EntitiesRel)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(ForeignKeyName);
    }
}