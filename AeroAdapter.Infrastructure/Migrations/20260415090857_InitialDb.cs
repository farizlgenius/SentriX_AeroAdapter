using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateChannels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n_channel_id = table.Column<short>(type: "smallint", nullable: false),
                    c_type = table.Column<short>(type: "smallint", nullable: false),
                    c_port = table.Column<short>(type: "smallint", nullable: false),
                    baudrate = table.Column<short>(type: "smallint", nullable: false),
                    timer_1 = table.Column<short>(type: "smallint", nullable: false),
                    timer_2 = table.Column<short>(type: "smallint", nullable: false),
                    c_model_id = table.Column<short>(type: "smallint", nullable: false),
                    c_rts_mode = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateChannels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLevelSpecifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n_ports = table.Column<short>(type: "smallint", nullable: false),
                    n_scps = table.Column<short>(type: "smallint", nullable: false),
                    n_timezones = table.Column<short>(type: "smallint", nullable: false),
                    n_holidays = table.Column<short>(type: "smallint", nullable: false),
                    b_direct_mode = table.Column<short>(type: "smallint", nullable: false),
                    debug_rq = table.Column<short>(type: "smallint", nullable: false),
                    n_debug_arg = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLevelSpecifications", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "CreateChannels",
                columns: new[] { "id", "baudrate", "c_model_id", "c_port", "c_rts_mode", "c_type", "created_at", "n_channel_id", "timer_1", "timer_2", "updated_at" },
                values: new object[] { 1, (short)0, (short)0, (short)0, (short)0, (short)7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)1, (short)3000, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SystemLevelSpecifications",
                columns: new[] { "id", "b_direct_mode", "created_at", "debug_rq", "n_debug_arg", "n_holidays", "n_ports", "n_scps", "n_timezones", "updated_at" },
                values: new object[] { 1, (short)1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)0, (short)0, (short)0, (short)1024, (short)1024, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateChannels");

            migrationBuilder.DropTable(
                name: "SystemLevelSpecifications");
        }
    }
}
