using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _03AlterColumsnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityCaracter",
                table: "EntityProperties",
                newName: "PropertyMaxLength");

            migrationBuilder.RenameColumn(
                name: "EntitieId",
                table: "Entities",
                newName: "EntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EntityProperties",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<string>(
                name: "NameEntite",
                table: "Entities",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyMaxLength",
                table: "EntityProperties",
                newName: "QuantityCaracter");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "Entities",
                newName: "EntitieId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EntityProperties",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");

            migrationBuilder.AlterColumn<string>(
                name: "NameEntite",
                table: "Entities",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");
        }
    }
}
