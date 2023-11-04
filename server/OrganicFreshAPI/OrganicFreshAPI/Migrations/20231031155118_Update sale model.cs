using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganicFreshAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updatesalemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedPayment",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedPayment",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ConfirmedPayment",
                table: "CheckoutsDetails");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "CheckoutsDetails");
        }
    }
}
