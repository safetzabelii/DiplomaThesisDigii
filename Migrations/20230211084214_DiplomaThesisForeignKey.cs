using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaThesisDigitalization.Migrations
{
    /// <inheritdoc />
    public partial class DiplomaThesisForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiplomaTheses_Titles_TitleId",
                table: "DiplomaTheses");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                table: "DiplomaTheses",
                newName: "TitleID");

            migrationBuilder.RenameIndex(
                name: "IX_DiplomaTheses_TitleId",
                table: "DiplomaTheses",
                newName: "IX_DiplomaTheses_TitleID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                table: "DiplomaTheses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_DiplomaTheses_Titles_TitleID",
                table: "DiplomaTheses",
                column: "TitleID",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiplomaTheses_Titles_TitleID",
                table: "DiplomaTheses");

            migrationBuilder.RenameColumn(
                name: "TitleID",
                table: "DiplomaTheses",
                newName: "TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_DiplomaTheses_TitleID",
                table: "DiplomaTheses",
                newName: "IX_DiplomaTheses_TitleId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                table: "DiplomaTheses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiplomaTheses_Titles_TitleId",
                table: "DiplomaTheses",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
