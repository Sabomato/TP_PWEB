using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class PropertyV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Properties_PropertyId",
                table: "Verification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Verification",
                table: "Verification");

            migrationBuilder.RenameTable(
                name: "Verification",
                newName: "Properties_ExitVerifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties_ExitVerifications",
                table: "Properties_ExitVerifications",
                columns: new[] { "PropertyId", "Id" });

            migrationBuilder.CreateTable(
                name: "Properties_EntranceVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    isChecked = table.Column<bool>(nullable: false),
                    Observation = table.Column<string>(nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_ExitVerifications_Properties_PropertyId",
                table: "Properties_ExitVerifications",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_ExitVerifications_Properties_PropertyId",
                table: "Properties_ExitVerifications");

            migrationBuilder.DropTable(
                name: "Properties_EntranceVerifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties_ExitVerifications",
                table: "Properties_ExitVerifications");

            migrationBuilder.RenameTable(
                name: "Properties_ExitVerifications",
                newName: "Verification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Verification",
                table: "Verification",
                columns: new[] { "PropertyId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Properties_PropertyId",
                table: "Verification",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
