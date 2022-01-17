using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_PWEB.Data.Migrations
{
    public partial class CategoryV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_propertyManagers_OwnerId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_propertyManagers",
                table: "propertyManagers");

            migrationBuilder.RenameTable(
                name: "propertyManagers",
                newName: "PropertyManagers");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyManagers",
                table: "PropertyManagers",
                column: "PropertyManagerId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyManagers_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "PropertyManagers",
                principalColumn: "PropertyManagerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyManagers_OwnerId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyManagers",
                table: "PropertyManagers");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "PropertyManagers",
                newName: "propertyManagers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_propertyManagers",
                table: "propertyManagers",
                column: "PropertyManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_propertyManagers_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "propertyManagers",
                principalColumn: "PropertyManagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
