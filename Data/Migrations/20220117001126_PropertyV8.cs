using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class PropertyV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId1",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerificationId1",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyId1",
                table: "Images",
                column: "PropertyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Images_VerificationId1",
                table: "Images",
                column: "VerificationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Properties_PropertyId1",
                table: "Images",
                column: "PropertyId1",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Verifications_VerificationId1",
                table: "Images",
                column: "VerificationId1",
                principalTable: "Verifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_PropertyId1",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Verifications_VerificationId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Images_PropertyId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VerificationId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PropertyId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VerificationId1",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId");
        }
    }
}
