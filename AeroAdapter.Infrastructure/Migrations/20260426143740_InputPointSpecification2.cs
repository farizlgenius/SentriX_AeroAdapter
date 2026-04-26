using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InputPointSpecification2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InputPointSpecifications",
                keyColumn: "id",
                keyValue: 1,
                column: "sio_number",
                value: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InputPointSpecifications",
                keyColumn: "id",
                keyValue: 1,
                column: "sio_number",
                value: (short)-1);
        }
    }
}
