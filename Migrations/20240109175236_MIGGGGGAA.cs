using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaThesisDigitalization.Migrations
{
    /// <inheritdoc />
    public partial class MIGGGGGAA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Departments_DepartmentId",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Departments_DepartmentId",
                table: "Fields",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Departments_DepartmentId",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Fields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Departments_DepartmentId",
                table: "Fields",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
