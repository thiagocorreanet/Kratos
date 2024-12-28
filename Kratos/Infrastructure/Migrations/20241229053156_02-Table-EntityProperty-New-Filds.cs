using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _02TableEntityPropertyNewFilds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityProperties_TypesData_TypeDataId",
                table: "EntityProperties");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "EntityProperties",
                newName: "TypeRel");

            migrationBuilder.AlterColumn<int>(
                name: "TypeDataId",
                table: "EntityProperties",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRequiredRel",
                table: "EntityProperties",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "TypeRel",
                table: "EntityProperties",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddForeignKey(
                name: "FK_ENTITYPROPERTY_TYPEDATA",
                table: "EntityProperties",
                column: "TypeDataId",
                principalTable: "TypesData",
                principalColumn: "TypeDataId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENTITYPROPERTY_TYPEDATA",
                table: "EntityProperties");

            migrationBuilder.RenameColumn(
                name: "TypeRel",
                table: "EntityProperties",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "TypeDataId",
                table: "EntityProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRequiredRel",
                table: "EntityProperties",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT")
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "EntityProperties",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityProperties_TypesData_TypeDataId",
                table: "EntityProperties",
                column: "TypeDataId",
                principalTable: "TypesData",
                principalColumn: "TypeDataId");
        }
    }
}
