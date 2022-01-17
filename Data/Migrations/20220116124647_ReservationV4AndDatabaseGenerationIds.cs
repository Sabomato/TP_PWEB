using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ReservationV4AndDatabaseGenerationIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isChecked",
                table: "Verifications",
                newName: "IsChecked");

            migrationBuilder.RenameColumn(
                name: "isAtExit",
                table: "Verifications",
                newName: "IsAtExit");

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

            migrationBuilder.RenameColumn(
                name: "IsChecked",
                table: "Verifications",
                newName: "isChecked");

            migrationBuilder.RenameColumn(
                name: "IsAtExit",
                table: "Verifications",
                newName: "isAtExit");
        }
    }
}
