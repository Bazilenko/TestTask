using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingServiceBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "RoomServices",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "RoomServices",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "BookingServices",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookingServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookingServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "BookingServices",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoomServices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomServices");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RoomServices");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookingServices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookingServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookingServices");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BookingServices");
        }
    }
}
