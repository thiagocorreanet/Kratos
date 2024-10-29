using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableEntityProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityProperties",
                columns: table => new
                {
                    EntityPropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    IsRequired = table.Column<bool>(type: "BIT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "GETDATE()"),
                    AlteredAt = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITYPROPERTIES", x => x.EntityPropertyId);
                    table.ForeignKey(
                        name: "FK_ENTITY_ENTITYPROPERTY",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "EntitieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperties_EntityId",
                table: "EntityProperties",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityProperties");
        }
    }
}
