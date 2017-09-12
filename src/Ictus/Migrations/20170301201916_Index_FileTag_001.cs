using Microsoft.EntityFrameworkCore.Migrations;

namespace Yio.Migrations
{
    public partial class Index_FileTag_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                table: "FileTags", 
                column: "Id",
                unique: true,
                name: "UIX_FileTags_Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UIX_FileTags_Id",
                table: "Files"
            );
        }
    }
}
