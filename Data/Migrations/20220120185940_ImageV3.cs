using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ImageV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Verifications_VerificationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VerificationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_VerificationId",
                table: "Images",
                column: "VerificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Verifications_VerificationId",
                table: "Images",
                column: "VerificationId",
                principalTable: "Verifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
