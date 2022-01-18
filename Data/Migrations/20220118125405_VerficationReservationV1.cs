using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class VerficationReservationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyEmployee_PropertyManagers_PropertyManagerId",
                table: "PropertyEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyEmployee",
                table: "PropertyEmployee");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Verifications");

            migrationBuilder.RenameTable(
                name: "PropertyEmployee",
                newName: "PropertyEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyEmployee_PropertyManagerId",
                table: "PropertyEmployees",
                newName: "IX_PropertyEmployees_PropertyManagerId");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Verifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VerificationReservationId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyEmployees",
                table: "PropertyEmployees",
                column: "PropertyEmployeeId");

            migrationBuilder.CreateTable(
                name: "VerificationReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VerificationId = table.Column<int>(nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    IsChecked = table.Column<bool>(nullable: false),
                    Observation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationReservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerificationReservations_Verifications_VerificationId",
                        column: x => x.VerificationId,
                        principalTable: "Verifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_VerificationReservationId",
                table: "Images",
                column: "VerificationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationReservations_ReservationId",
                table: "VerificationReservations",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationReservations_VerificationId",
                table: "VerificationReservations",
                column: "VerificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_VerificationReservations_VerificationReservationId",
                table: "Images",
                column: "VerificationReservationId",
                principalTable: "VerificationReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyEmployees_PropertyManagers_PropertyManagerId",
                table: "PropertyEmployees",
                column: "PropertyManagerId",
                principalTable: "PropertyManagers",
                principalColumn: "PropertyManagerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_VerificationReservations_VerificationReservationId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyEmployees_PropertyManagers_PropertyManagerId",
                table: "PropertyEmployees");

            migrationBuilder.DropTable(
                name: "VerificationReservations");

            migrationBuilder.DropIndex(
                name: "IX_Images_VerificationReservationId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyEmployees",
                table: "PropertyEmployees");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "VerificationReservationId",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "PropertyEmployees",
                newName: "PropertyEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyEmployees_PropertyManagerId",
                table: "PropertyEmployee",
                newName: "IX_PropertyEmployee_PropertyManagerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Verifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Verifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyEmployee",
                table: "PropertyEmployee",
                column: "PropertyEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyEmployee_PropertyManagers_PropertyManagerId",
                table: "PropertyEmployee",
                column: "PropertyManagerId",
                principalTable: "PropertyManagers",
                principalColumn: "PropertyManagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
