using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ReservationV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Verifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_ReservationId",
                table: "Verifications",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Reservations_ReservationId",
                table: "Verifications",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Reservations_ReservationId",
                table: "Verifications");

            migrationBuilder.DropIndex(
                name: "IX_Verifications_ReservationId",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Verifications");
        }
    }
}
