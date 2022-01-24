using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABS_WebAPI.Migrations
{
    public partial class inintialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GetAvailableFlights",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: true),
                    AirlineName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Origin = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GetIds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GetNames",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GetSeatNumbers",
                columns: table => new
                {
                    Row = table.Column<short>(nullable: false),
                    Column = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    AirlineId = table.Column<int>(nullable: false),
                    OriginId = table.Column<int>(nullable: false),
                    DestinationId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airline_Name",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Airport_Destination",
                        column: x => x.DestinationId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Airport_Origin",
                        column: x => x.OriginId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightSection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatClass = table.Column<short>(nullable: false),
                    FlightId = table.Column<string>(unicode: false, maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flight_Id",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROW = table.Column<short>(nullable: false),
                    COLUMN = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    FlightSectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightSection_Id",
                        column: x => x.FlightSectionId,
                        principalTable: "FlightSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "CHK_Unique_AirlineName",
                table: "Airline",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CHK_Unique_AirportName",
                table: "Airport",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineId",
                table: "Flight",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_DestinationId",
                table: "Flight",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_OriginId",
                table: "Flight",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSection_FlightId",
                table: "FlightSection",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_FlightSectionId",
                table: "Seat",
                column: "FlightSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetAvailableFlights");

            migrationBuilder.DropTable(
                name: "GetIds");

            migrationBuilder.DropTable(
                name: "GetNames");

            migrationBuilder.DropTable(
                name: "GetSeatNumbers");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "FlightSection");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropTable(
                name: "Airport");
        }
    }
}
