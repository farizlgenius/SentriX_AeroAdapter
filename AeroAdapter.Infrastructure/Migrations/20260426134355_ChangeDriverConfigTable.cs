using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDriverConfigTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DriverConfigurations",
                keyColumn: "id",
                keyValue: 1,
                column: "msp1_number",
                value: (short)0);

            migrationBuilder.UpdateData(
                table: "ScpDeviceSpecifications",
                keyColumn: "id",
                keyValue: 1,
                column: "n_sio",
                value: (short)33);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DriverConfigurations",
                keyColumn: "id",
                keyValue: 1,
                column: "msp1_number",
                value: (short)3);

            migrationBuilder.UpdateData(
                table: "ScpDeviceSpecifications",
                keyColumn: "id",
                keyValue: 1,
                column: "n_sio",
                value: (short)15);
        }
    }
}
