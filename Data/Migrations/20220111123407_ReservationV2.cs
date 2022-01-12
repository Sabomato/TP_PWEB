using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class ReservationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_OwnerId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_ExitVerifications_Properties_PropertyId",
                table: "Properties_ExitVerifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Evaluation_EvaluationId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "Properties_EntranceVerifications");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_EvaluationId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties_ExitVerifications",
                table: "Properties_ExitVerifications");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Properties_ExitVerifications");

            migrationBuilder.RenameTable(
                name: "Properties_ExitVerifications",
                newName: "Verification");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Evaluation",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientReservationId",
                table: "Evaluation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StayReservationId",
                table: "Evaluation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "EntrancePropertyId",
                table: "Verification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExitPropertyId",
                table: "Verification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Verification",
                table: "Verification",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PropertyManager",
                columns: table => new
                {
                    PropertyManagerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyManager", x => x.PropertyManagerId);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Verification_EntrancePropertyId",
                table: "Verification",
                column: "EntrancePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_ExitPropertyId",
                table: "Verification",
                column: "ExitPropertyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyManager_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "PropertyManager",
                principalColumn: "PropertyManagerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluation_Reservation_StayReservationId",
                table: "Evaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyManager_OwnerId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Properties_EntrancePropertyId",
                table: "Verification");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Properties_ExitPropertyId",
                table: "Verification");

            migrationBuilder.DropTable(
                name: "PropertyManager");

            migrationBuilder.DropIndex(
                name: "IX_Evaluation_ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropIndex(
                name: "IX_Evaluation_StayReservationId",
                table: "Evaluation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Verification",
                table: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Verification_EntrancePropertyId",
                table: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Verification_ExitPropertyId",
                table: "Verification");

            migrationBuilder.DropColumn(
                name: "ClientReservationId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "StayReservationId",
                table: "Evaluation");

            migrationBuilder.DropColumn(
                name: "EntrancePropertyId",
                table: "Verification");

            migrationBuilder.DropColumn(
                name: "ExitPropertyId",
                table: "Verification");

            migrationBuilder.RenameTable(
                name: "Verification",
                newName: "Properties_ExitVerifications");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EvaluationId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Evaluation",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Properties_ExitVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties_ExitVerifications",
                table: "Properties_ExitVerifications",
                columns: new[] { "PropertyId", "Id" });

            migrationBuilder.CreateTable(
                name: "Properties_EntranceVerifications",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isChecked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties_EntranceVerifications", x => new { x.PropertyId, x.Id });
                    table.ForeignKey(
                        name: "FK_Properties_EntranceVerifications_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_EvaluationId",
                table: "Reservation",
                column: "EvaluationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_ExitVerifications_Properties_PropertyId",
                table: "Properties_ExitVerifications",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Evaluation_EvaluationId",
                table: "Reservation",
                column: "EvaluationId",
                principalTable: "Evaluation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
