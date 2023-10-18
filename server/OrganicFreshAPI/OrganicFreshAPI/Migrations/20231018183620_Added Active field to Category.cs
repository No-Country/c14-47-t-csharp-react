using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganicFreshAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedActivefieldtoCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Categories");
        }
    }
}
