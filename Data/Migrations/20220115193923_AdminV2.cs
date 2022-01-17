using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class AdminV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Admins_AdminId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Admins_AdminId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyManagers_Admins_AdminId",
                table: "PropertyManagers");

            migrationBuilder.DropIndex(
                name: "IX_PropertyManagers_AdminId",
                table: "PropertyManagers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_AdminId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AdminId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "PropertyManagers");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "PropertyManagers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyManagers_AdminId",
                table: "PropertyManagers",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AdminId",
                table: "Clients",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AdminId",
                table: "Categories",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Admins_AdminId",
                table: "Categories",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Admins_AdminId",
                table: "Clients",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyManagers_Admins_AdminId",
                table: "PropertyManagers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
