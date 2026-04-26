using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InputPointSpecification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputPointSpecifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scp_id = table.Column<short>(type: "smallint", nullable: false),
                    mac = table.Column<string>(type: "text", nullable: false),
                    sio_number = table.Column<short>(type: "smallint", nullable: false),
                    input_number = table.Column<short>(type: "smallint", nullable: false),
                    icvt_num = table.Column<short>(type: "smallint", nullable: false),
                    debounce = table.Column<short>(type: "smallint", nullable: false),
                    hold_time = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputPointSpecifications", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "InputPointSpecifications",
                columns: new[] { "id", "created_at", "debounce", "hold_time", "icvt_num", "input_number", "mac", "scp_id", "sio_number", "updated_at" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)2, (short)5, (short)0, (short)-1, "", (short)0, (short)-1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputPointSpecifications");
        }
    }
}
