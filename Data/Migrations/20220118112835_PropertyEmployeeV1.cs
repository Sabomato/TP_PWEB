using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class PropertyEmployeeV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyEmployee",
                columns: table => new
                {
                    PropertyEmployeeId = table.Column<string>(nullable: false),
                    PropertyManagerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyEmployee", x => x.PropertyEmployeeId);
                    table.ForeignKey(
                        name: "FK_PropertyEmployee_PropertyManagers_PropertyManagerId",
                        column: x => x.PropertyManagerId,
                        principalTable: "PropertyManagers",
                        principalColumn: "PropertyManagerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEmployee_PropertyManagerId",
                table: "PropertyEmployee",
                column: "PropertyManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyEmployee");
        }
    }
}
