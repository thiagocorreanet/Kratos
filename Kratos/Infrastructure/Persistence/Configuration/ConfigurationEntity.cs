

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ConfigurationEntity : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.ToTable("Entities");

            builder.HasKey(x => x.Id)
                .HasName("PK_ENTITIES");

             builder.Property(c => c.Id)
                .HasColumnName("EntitieId")
                .HasColumnOrder(1)
                .ValueGeneratedOnAdd();

             builder.Property(c => c.Name)
               .HasColumnName("NameEntite")
               .HasColumnOrder(2)
               .IsRequired()
               .HasColumnType("VARCHAR(50)");

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

            builder.HasIndex(c => c.Id)
                .HasDatabaseName("IX_ENTITIE_ID")
                .IsUnique();
        }
    }
}