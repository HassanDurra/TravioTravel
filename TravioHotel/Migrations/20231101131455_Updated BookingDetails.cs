using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravioHotel.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "air_craft_id",
                table: "BookingFlightDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "air_craft_id",
                table: "BookingFlightDetails");
        }
    }
}
