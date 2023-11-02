using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravioHotel.Migrations
{
    /// <inheritdoc />
    public partial class FlightDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arrival_date",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "booking_code",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "status",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "BookingRequests");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BookingRequests",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "BookingClientDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flight_details_id = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passport_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnic_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    date_of_birth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_booked = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingClientDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BookingFlightDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airline_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    airline_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    journey_type = table.Column<int>(type: "int", nullable: false),
                    from = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    to = table.Column<int>(type: "int", nullable: false),
                    departure_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departure_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arrival_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arrival_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flight_duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_price = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFlightDetails", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingClientDetails");

            migrationBuilder.DropTable(
                name: "BookingFlightDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BookingRequests",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "arrival_date",
                table: "BookingRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "booking_code",
                table: "BookingRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "BookingRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "BookingRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
