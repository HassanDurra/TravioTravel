using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravioHotel.Migrations
{
    /// <inheritdoc />
    public partial class updatedCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "capital",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "currency_symbol",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emoji",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iso2",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iso3",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "native",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phonecode",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "region",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subregion",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "timezone",
                table: "Countries",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tld",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "capital",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "currency_symbol",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "emoji",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "iso2",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "iso3",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "native",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "phonecode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "region",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "subregion",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "timezone",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "tld",
                table: "Countries");
        }
    }
}
