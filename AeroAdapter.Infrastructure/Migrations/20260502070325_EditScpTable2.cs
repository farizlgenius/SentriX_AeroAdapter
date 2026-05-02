using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditScpTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fw",
                table: "Scps");

            migrationBuilder.DropColumn(
                name: "ip",
                table: "Scps");

            migrationBuilder.DropColumn(
                name: "port",
                table: "Scps");

            migrationBuilder.DropColumn(
                name: "serial_number",
                table: "Scps");

            migrationBuilder.DropColumn(
                name: "sync_status",
                table: "Scps");

            migrationBuilder.DropColumn(
                name: "synced_at",
                table: "Scps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fw",
                table: "Scps",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ip",
                table: "Scps",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "port",
                table: "Scps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "serial_number",
                table: "Scps",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sync_status",
                table: "Scps",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "synced_at",
                table: "Scps",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW() AT TIME ZONE 'UTC'");
        }
    }
}
