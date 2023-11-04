using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganicFreshAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedcheckoutdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedPayment",
                table: "CheckoutsDetails");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "CheckoutsDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedPayment",
                table: "CheckoutsDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "CheckoutsDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
