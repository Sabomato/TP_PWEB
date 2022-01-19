using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ReservationV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Reservations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Verifications",
                type: "int",
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
    }
}
