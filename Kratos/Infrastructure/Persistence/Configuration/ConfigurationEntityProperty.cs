using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ConfigurationEntityProperty : IEntityTypeConfiguration<EntityProperty>
{
   public void Configure(EntityTypeBuilder<EntityProperty> builder)
    {
        builder.ToTable("EntityProperties");

        builder.HasKey(x => x.Id)
            .HasName("PK_ENTITYPROPERTIES");

        builder.Property(c => c.Id)
            .HasColumnName("EntityPropertyId")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.EntityId) 
            .HasColumnName(nameof(EntityProperty.EntityId))
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("INT");

        builder.HasOne(c => c.EntityRel) 
            .WithMany(e => e.PropertyRel) 
            .HasForeignKey(c => c.EntityId) 
            .HasConstraintName("FK_ENTITY_ENTITYPROPERTY") 
            .OnDelete(DeleteBehavior.Cascade); 

        builder.Property(c => c.Name)
            .HasColumnName(nameof(EntityProperty.Name))
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");

        builder.Property(c => c.Type)
            .HasColumnName(nameof(EntityProperty.Type))
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");

        builder.Property(c => c.IsRequired)
            .HasColumnName(nameof(EntityProperty.IsRequired))
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("BIT");

        builder.Property(c => c.QuantityCaracter)
           .HasColumnName(nameof(EntityProperty.QuantityCaracter))
           .HasColumnOrder(6)
           .IsRequired(true)
           .HasColumnType("INT");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnOrder(7)
            .IsRequired()
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(c => c.AlteredAt)
            .HasColumnName("AlteredAt")
            .HasColumnOrder(8)
            .IsRequired()
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

    }
}