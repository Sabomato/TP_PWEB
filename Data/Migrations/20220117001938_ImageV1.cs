using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ImageV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_PropertyId1",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Verifications_VerificationId1",
                table: "Images");

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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VerificationId",
                table: "Images",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Images",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId1",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerificationId1",
                table: "Images",
                type: "int",
                nullable: true);

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
    }
}
