using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class EvaluationV111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_StayReservationId",
                table: "Evaluation");

            migrationBuilder.DropIndex(
                name: "IX_Evaluation_ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropIndex(
                name: "IX_Evaluation_StayReservationId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "StayReservationId",
                table: "Evaluation");

            migrationBuilder.AddColumn<int>(
                name: "ClientEvaluationId",
                table: "Reservation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StayEvaluationId",
                table: "Reservation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClientEvaluationId",
                table: "Reservation",
                column: "ClientEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_StayEvaluationId",
                table: "Reservation",
                column: "StayEvaluationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Evaluation_ClientEvaluationId",
                table: "Reservation",
                column: "ClientEvaluationId",
                principalTable: "Evaluation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Evaluation_StayEvaluationId",
                table: "Reservation",
                column: "StayEvaluationId",
                principalTable: "Evaluation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Evaluation_ClientEvaluationId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Evaluation_StayEvaluationId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ClientEvaluationId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_StayEvaluationId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ClientEvaluationId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "StayEvaluationId",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ClientReservationId",
                table: "Evaluation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StayReservationId",
                table: "Evaluation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_ClientReservationId",
                table: "Evaluation",
                column: "ClientReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_StayReservationId",
                table: "Evaluation",
                column: "StayReservationId",
                unique: true);

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
    }
}
