using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravioHotel.Migrations
{
    /// <inheritdoc />
    public partial class Aircraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    agent_id = table.Column<int>(type: "int", nullable: false),
                    aircraft_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aircraft_model_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aircraft_model_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_seats = table.Column<int>(type: "int", nullable: false),
                    remaining_seats = table.Column<int>(type: "int", nullable: false),
                    bussiness_seats = table.Column<int>(type: "int", nullable: false),
                    economy_seats = table.Column<int>(type: "int", nullable: false),
                    first_class_seats = table.Column<int>(type: "int", nullable: false),
                    bussiness_seats_occupied = table.Column<int>(type: "int", nullable: false),
                    economy_seats_occupied = table.Column<int>(type: "int", nullable: false),
                    first_class_seats_occupied = table.Column<int>(type: "int", nullable: false),
                    bussiness_seats_remaining = table.Column<int>(type: "int", nullable: false),
                    economy_seats_remaining = table.Column<int>(type: "int", nullable: false),
                    first_class_seats_remaining = table.Column<int>(type: "int", nullable: false),
                    availibility = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aircrafts");
        }
    }
}
