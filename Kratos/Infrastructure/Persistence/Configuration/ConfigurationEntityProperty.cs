using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ConfigurationEntityProperty : IEntityTypeConfiguration<EntityProperty>
{
    private const string ForeignKeyTypeData = "FK_ENTITYPROPERTY_TYPEDATA";
    private const string ForeignKeyEntity = "FK_ENTITY_ENTITYPROPERTY";
    
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
            .IsRequired(true)
            .HasColumnType("INT");

        builder.Property(c => c.Name)
            .HasColumnName(nameof(EntityProperty.Name))
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");

        builder.Property(c => c.TypeDataId)
            .HasColumnName(nameof(EntityProperty.TypeDataId))
            .HasColumnOrder(4)
            .IsRequired(true)
            .HasColumnType("INT");

        builder.Property(c => c.IsRequired)
            .HasColumnName(nameof(EntityProperty.IsRequired))
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("BIT");

        builder.Property(c => c.PropertyMaxLength)
           .HasColumnName(nameof(EntityProperty.PropertyMaxLength))
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
        
        builder.Property(c => c.IsRequiredRel)
            .HasColumnName(nameof(EntityProperty.IsRequiredRel))
            .HasColumnOrder(9)
            .IsRequired()
            .HasColumnType("BIT");
        
        builder.Property(c => c.TypeRel)
            .HasColumnName(nameof(EntityProperty.TypeRel))
            .HasColumnOrder(10)
            .IsRequired()
            .HasColumnType("VARCHAR(50)");
        
        builder.Property(c => c.EntityIdRel) 
            .HasColumnName(nameof(EntityProperty.EntityIdRel))
            .HasColumnOrder(11)
            .IsRequired(false)
            .HasColumnType("INT");
        
        builder.HasOne(b => b.TypeDataRel)
            .WithMany(c => c.PropertiesRel)
            .HasForeignKey(b => b.TypeDataId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(ForeignKeyTypeData);
        
        builder.HasOne(c => c.EntityRel) 
            .WithMany(e => e.PropertyRel) 
            .HasForeignKey(c => c.EntityId) 
            .HasConstraintName(ForeignKeyEntity) 
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade); 
        
        builder.HasOne(c => c.EntityRel) 
            .WithMany(e => e.PropertyRel) 
            .HasForeignKey(c => c.EntityIdRel) 
            .HasConstraintName(ForeignKeyEntity) 
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade); 

    }
}