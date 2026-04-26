using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDriverConfigTableAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DriverConfigurations",
                keyColumn: "id",
                keyValue: 1,
                column: "port_number",
                value: (short)3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DriverConfigurations",
                keyColumn: "id",
                keyValue: 1,
                column: "port_number",
                value: (short)0);
        }
    }
}
