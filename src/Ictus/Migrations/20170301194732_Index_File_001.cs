using Microsoft.EntityFrameworkCore.Migrations;

namespace Ictus.Migrations
{
    public partial class Index_File_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                table: "Files",
                column: "Id",
                unique: true,
                name: "UIX_Files_Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UIX_Files_Id",
                table: "Files"
            );
        }
    }
}
