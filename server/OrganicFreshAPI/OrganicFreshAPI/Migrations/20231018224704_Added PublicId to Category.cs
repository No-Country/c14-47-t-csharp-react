using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganicFreshAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedPublicIdtoCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Categories");
        }
    }
}
