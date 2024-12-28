using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ConfigurationTypeData : IEntityTypeConfiguration<TypeData>
{
    private const string TableName = "TypesData";
    private const string PrimaryKeyName = "PK_TYPEDATA";
    private const string IndexName = "IX_TYPEDATA_ID";
    private const string DefaultDateSql = "GETDATE()";
    
    public void Configure(EntityTypeBuilder<TypeData> builder)
    {
        builder.ToTable(TableName);
        
        builder.HasKey(x => x.Id)
            .HasName(PrimaryKeyName);
        
        builder.Property(c => c.Id)
            .HasColumnName("TypeDataId")
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
            .HasDefaultValueSql(DefaultDateSql)
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(x => x.AlteredAt)
            .HasColumnOrder(4)
            .IsRequired(true)
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql(DefaultDateSql)
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasIndex(c => c.Id)
            .HasDatabaseName(IndexName)
            .IsUnique();
    }
}