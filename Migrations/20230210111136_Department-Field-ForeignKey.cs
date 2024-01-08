using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaThesisDigitalization.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentFieldForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Fields",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_DepartmentId",
                table: "Fields",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Departments_DepartmentId",
                table: "Fields",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Departments_DepartmentId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_DepartmentId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Fields");
        }
    }
}
