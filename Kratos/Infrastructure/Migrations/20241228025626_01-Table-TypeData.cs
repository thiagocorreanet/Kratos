using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _01TableTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequiredRel",
                table: "EntityProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TypeDataId",
                table: "EntityProperties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypesData",
                columns: table => new
                {
                    TypeDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "GETDATE()"),
                    AlteredAt = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPEDATA", x => x.TypeDataId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperties_TypeDataId",
                table: "EntityProperties",
                column: "TypeDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TYPEDATA_ID",
                table: "TypesData",
                column: "TypeDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityProperties_TypesData_TypeDataId",
                table: "EntityProperties",
                column: "TypeDataId",
                principalTable: "TypesData",
                principalColumn: "TypeDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityProperties_TypesData_TypeDataId",
                table: "EntityProperties");

            migrationBuilder.DropTable(
                name: "TypesData");

            migrationBuilder.DropIndex(
                name: "IX_EntityProperties_TypeDataId",
                table: "EntityProperties");

            migrationBuilder.DropColumn(
                name: "IsRequiredRel",
                table: "EntityProperties");

            migrationBuilder.DropColumn(
                name: "TypeDataId",
                table: "EntityProperties");
        }
    }
}
