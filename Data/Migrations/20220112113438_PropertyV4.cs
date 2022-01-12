using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class PropertyV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Client_ClientId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Properties_EntrancePropertyId",
                table: "Verification");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Properties_ExitPropertyId",
                table: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Verification_EntrancePropertyId",
                table: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Verification_ExitPropertyId",
                table: "Verification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "EntrancePropertyId",
                table: "Verification");

            migrationBuilder.DropColumn(
                name: "ExitPropertyId",
                table: "Verification");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Verification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_PropertyId",
                table: "Verification",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Clients_ClientId",
                table: "Reservation",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Properties_PropertyId",
                table: "Verification",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Clients_ClientId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Properties_PropertyId",
                table: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Verification_PropertyId",
                table: "Verification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Verification");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.AddColumn<int>(
                name: "EntrancePropertyId",
                table: "Verification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExitPropertyId",
                table: "Verification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_EntrancePropertyId",
                table: "Verification",
                column: "EntrancePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_ExitPropertyId",
                table: "Verification",
                column: "ExitPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Client_ClientId",
                table: "Reservation",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Properties_EntrancePropertyId",
                table: "Verification",
                column: "EntrancePropertyId",
                principalTable: "Properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Properties_ExitPropertyId",
                table: "Verification",
                column: "ExitPropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
