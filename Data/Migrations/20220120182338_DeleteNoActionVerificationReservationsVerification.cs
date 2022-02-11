using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class DeleteNoActionVerificationReservationsVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationReservations_Verifications_VerificationId",
                table: "VerificationReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationReservations_Verifications_VerificationId",
                table: "VerificationReservations",
                column: "VerificationId",
                principalTable: "Verifications",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationReservations_Verifications_VerificationId",
                table: "VerificationReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationReservations_Verifications_VerificationId",
                table: "VerificationReservations",
                column: "VerificationId",
                principalTable: "Verifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
