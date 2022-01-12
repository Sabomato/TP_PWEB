using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class EvaluationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_StayReservationId",
                table: "Evaluation");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_Reservation_ClientReservationId",
                table: "Evaluation",
                column: "ClientReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_Reservation_StayReservationId",
                table: "Evaluation",
                column: "StayReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_StayReservationId",
                table: "Evaluation");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_Reservation_ClientReservationId",
                table: "Evaluation",
                column: "ClientReservationId",
                principalTable: "Reservation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluation_Reservation_StayReservationId",
                table: "Evaluation",
                column: "StayReservationId",
                principalTable: "Reservation",
                principalColumn: "Id");
        }
    }
}
