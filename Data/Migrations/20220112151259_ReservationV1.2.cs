using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ReservationV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Evaluations_ClientEvaluationId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Clients_ClientId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Evaluations_StayEvaluationId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_StayEvaluationId",
                table: "Reservations",
                newName: "IX_Reservations_StayEvaluationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PropertyId",
                table: "Reservations",
                newName: "IX_Reservations_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_ClientId",
                table: "Reservations",
                newName: "IX_Reservations_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_ClientEvaluationId",
                table: "Reservations",
                newName: "IX_Reservations_ClientEvaluationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Evaluations_ClientEvaluationId",
                table: "Reservations",
                column: "ClientEvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_ClientId",
                table: "Reservations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Properties_PropertyId",
                table: "Reservations",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Evaluations_StayEvaluationId",
                table: "Reservations",
                column: "StayEvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Evaluations_ClientEvaluationId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_ClientId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Properties_PropertyId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Evaluations_StayEvaluationId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_StayEvaluationId",
                table: "Reservation",
                newName: "IX_Reservation_StayEvaluationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PropertyId",
                table: "Reservation",
                newName: "IX_Reservation_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservation",
                newName: "IX_Reservation_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ClientEvaluationId",
                table: "Reservation",
                newName: "IX_Reservation_ClientEvaluationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Evaluations_ClientEvaluationId",
                table: "Reservation",
                column: "ClientEvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Clients_ClientId",
                table: "Reservation",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Evaluations_StayEvaluationId",
                table: "Reservation",
                column: "StayEvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
