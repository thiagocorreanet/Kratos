using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnProjectIdInEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Entities",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ProjectId",
                table: "Entities",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ENTITY_PROJECT",
                table: "Entities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENTITY_PROJECT",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_ProjectId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Entities");
        }
    }
}
