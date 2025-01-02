using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _05TableEntitiesPropertiesEntityIdRequiredEntityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENTITY_ENTITYPROPERTY",
                table: "EntityProperties");

            migrationBuilder.DropIndex(
                name: "IX_EntityProperties_EntityId",
                table: "EntityProperties");

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "EntityProperties",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityIdRel",
                table: "EntityProperties",
                type: "INT",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperties_EntityIdRel",
                table: "EntityProperties",
                column: "EntityIdRel");

            migrationBuilder.AddForeignKey(
                name: "FK_ENTITY_ENTITYPROPERTY",
                table: "EntityProperties",
                column: "EntityIdRel",
                principalTable: "Entities",
                principalColumn: "EntityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENTITY_ENTITYPROPERTY",
                table: "EntityProperties");

            migrationBuilder.DropIndex(
                name: "IX_EntityProperties_EntityIdRel",
                table: "EntityProperties");

            migrationBuilder.DropColumn(
                name: "EntityIdRel",
                table: "EntityProperties");

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "EntityProperties",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperties_EntityId",
                table: "EntityProperties",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ENTITY_ENTITYPROPERTY",
                table: "EntityProperties",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "EntityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
