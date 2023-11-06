using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravioHotel.Migrations
{
    /// <inheritdoc />
    public partial class PaymentandBookingNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "created_at",
                table: "BookingClientDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Booking_Number",
                table: "BookingClientDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payment_method",
                table: "BookingClientDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booking_Number",
                table: "BookingClientDetails");

            migrationBuilder.DropColumn(
                name: "payment_method",
                table: "BookingClientDetails");

            migrationBuilder.AlterColumn<string>(
                name: "created_at",
                table: "BookingClientDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
