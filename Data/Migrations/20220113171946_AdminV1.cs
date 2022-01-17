using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class AdminV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "PropertyManagers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Admins");

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
    }
}
