using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyNoodles_ManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureUrlForItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Items",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Items");
        }
    }
}
