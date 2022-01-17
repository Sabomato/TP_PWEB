using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class VerificationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Reservations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceived",
                table: "Reservations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "Images",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Verifications_VerificationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VerificationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsReceived",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "Images");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
