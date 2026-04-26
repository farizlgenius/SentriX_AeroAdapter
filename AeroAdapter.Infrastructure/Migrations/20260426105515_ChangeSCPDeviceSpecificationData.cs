using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSCPDeviceSpecificationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ScpDeviceSpecifications",
                keyColumn: "id",
                keyValue: 1,
                column: "n_sio",
                value: (short)15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ScpDeviceSpecifications",
                keyColumn: "id",
                keyValue: 1,
                column: "n_sio",
                value: (short)33);
        }
    }
}
