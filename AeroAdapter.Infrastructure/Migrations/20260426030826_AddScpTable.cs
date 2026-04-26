using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AeroAdapter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessDatabaseSpecifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scp_id = table.Column<short>(type: "smallint", nullable: false),
                    mac = table.Column<string>(type: "text", nullable: false),
                    n_card = table.Column<short>(type: "smallint", nullable: false),
                    n_alvl = table.Column<short>(type: "smallint", nullable: false),
                    n_pin_digits = table.Column<short>(type: "smallint", nullable: false),
                    b_issue_code = table.Column<short>(type: "smallint", nullable: false),
                    b_apb_location = table.Column<short>(type: "smallint", nullable: false),
                    b_act_date = table.Column<short>(type: "smallint", nullable: false),
                    b_deact_date = table.Column<short>(type: "smallint", nullable: false),
                    b_vacation_date = table.Column<short>(type: "smallint", nullable: false),
                    b_upgrade_date = table.Column<short>(type: "smallint", nullable: false),
                    b_user_level = table.Column<short>(type: "smallint", nullable: false),
                    b_use_limit = table.Column<short>(type: "smallint", nullable: false),
                    b_support_time_apb = table.Column<short>(type: "smallint", nullable: false),
                    n_tz = table.Column<short>(type: "smallint", nullable: false),
                    b_asset_group = table.Column<short>(type: "smallint", nullable: false),
                    n_host_response_timeout = table.Column<short>(type: "smallint", nullable: false),
                    n_alvl_use4arg = table.Column<short>(type: "smallint", nullable: false),
                    n_escort_timeout = table.Column<short>(type: "smallint", nullable: false),
                    n_multi_card_timeout = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessDatabaseSpecifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DriverConfigurations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scp_id = table.Column<short>(type: "smallint", nullable: false),
                    mac = table.Column<string>(type: "text", nullable: false),
                    msp1_number = table.Column<short>(type: "smallint", nullable: false),
                    port_number = table.Column<short>(type: "smallint", nullable: false),
                    baudrate = table.Column<short>(type: "smallint", nullable: false),
                    reply_time = table.Column<short>(type: "smallint", nullable: false),
                    n_protocol = table.Column<short>(type: "smallint", nullable: false),
                    n_dialect = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverConfigurations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ScpDeviceSpecifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scp_id = table.Column<short>(type: "smallint", nullable: false),
                    mac = table.Column<string>(type: "text", nullable: false),
                    n_msp1_port = table.Column<short>(type: "smallint", nullable: false),
                    n_transcations = table.Column<int>(type: "integer", nullable: false),
                    n_sio = table.Column<short>(type: "smallint", nullable: false),
                    n_mp = table.Column<short>(type: "smallint", nullable: false),
                    n_cp = table.Column<short>(type: "smallint", nullable: false),
                    n_acr = table.Column<short>(type: "smallint", nullable: false),
                    n_alvl = table.Column<short>(type: "smallint", nullable: false),
                    n_trgr = table.Column<short>(type: "smallint", nullable: false),
                    n_proc = table.Column<short>(type: "smallint", nullable: false),
                    gmt_offset = table.Column<short>(type: "smallint", nullable: false),
                    n_dst_id = table.Column<short>(type: "smallint", nullable: false),
                    n_tz = table.Column<short>(type: "smallint", nullable: false),
                    n_hol = table.Column<short>(type: "smallint", nullable: false),
                    n_mpg = table.Column<short>(type: "smallint", nullable: false),
                    n_tran_limit = table.Column<int>(type: "integer", nullable: false),
                    n_oper_mode = table.Column<short>(type: "smallint", nullable: false),
                    oper_type = table.Column<short>(type: "smallint", nullable: false),
                    n_language = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScpDeviceSpecifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Scps",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scp_number = table.Column<short>(type: "smallint", nullable: false),
                    mac = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SioPanelConfigurations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scp_id = table.Column<short>(type: "smallint", nullable: false),
                    mac = table.Column<string>(type: "text", nullable: false),
                    sio_number = table.Column<short>(type: "smallint", nullable: false),
                    n_inputs = table.Column<short>(type: "smallint", nullable: false),
                    n_outputs = table.Column<short>(type: "smallint", nullable: false),
                    n_readers = table.Column<short>(type: "smallint", nullable: false),
                    model = table.Column<short>(type: "smallint", nullable: false),
                    enable = table.Column<short>(type: "smallint", nullable: false),
                    port = table.Column<short>(type: "smallint", nullable: false),
                    address = table.Column<short>(type: "smallint", nullable: false),
                    emax = table.Column<short>(type: "smallint", nullable: false),
                    flags = table.Column<short>(type: "smallint", nullable: false),
                    n_sio_next_in = table.Column<short>(type: "smallint", nullable: false),
                    n_sio_next_out = table.Column<short>(type: "smallint", nullable: false),
                    n_sio_next_rdr = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SioPanelConfigurations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WriterAudits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mac = table.Column<string>(type: "text", nullable: false),
                    scp_id = table.Column<int>(type: "integer", nullable: false),
                    command_code = table.Column<int>(type: "integer", nullable: false),
                    command = table.Column<string>(type: "text", nullable: false),
                    tag = table.Column<int>(type: "integer", nullable: false),
                    send_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    is_nak = table.Column<bool>(type: "boolean", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterAudits", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "AccessDatabaseSpecifications",
                columns: new[] { "id", "b_act_date", "b_apb_location", "b_asset_group", "b_deact_date", "b_issue_code", "b_support_time_apb", "b_upgrade_date", "b_use_limit", "b_user_level", "b_vacation_date", "created_at", "mac", "n_alvl", "n_alvl_use4arg", "n_card", "n_escort_timeout", "n_host_response_timeout", "n_multi_card_timeout", "n_pin_digits", "n_tz", "scp_id", "updated_at" },
                values: new object[] { 1, (short)2, (short)1, (short)0, (short)2, (short)1, (short)1, (short)1, (short)1, (short)7, (short)1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", (short)8, (short)0, (short)1000, (short)15, (short)5, (short)15, (short)324, (short)64, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "DriverConfigurations",
                columns: new[] { "id", "baudrate", "created_at", "mac", "msp1_number", "n_dialect", "n_protocol", "port_number", "reply_time", "scp_id", "updated_at" },
                values: new object[] { 1, (short)-1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", (short)3, (short)0, (short)0, (short)0, (short)0, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ScpDeviceSpecifications",
                columns: new[] { "id", "created_at", "gmt_offset", "mac", "n_acr", "n_alvl", "n_cp", "n_dst_id", "n_hol", "n_language", "n_mp", "n_mpg", "n_msp1_port", "n_oper_mode", "n_proc", "n_sio", "n_tran_limit", "n_transcations", "n_trgr", "n_tz", "oper_type", "scp_id", "updated_at" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)-25200, "", (short)64, (short)32000, (short)388, (short)0, (short)255, (short)0, (short)615, (short)128, (short)3, (short)0, (short)1024, (short)33, 60000, 60000, (short)1024, (short)255, (short)1, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SioPanelConfigurations",
                columns: new[] { "id", "address", "created_at", "emax", "enable", "flags", "mac", "model", "n_inputs", "n_outputs", "n_readers", "n_sio_next_in", "n_sio_next_out", "n_sio_next_rdr", "port", "scp_id", "sio_number", "updated_at" },
                values: new object[] { 1, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)3, (short)1, (short)0, "", (short)196, (short)7, (short)4, (short)4, (short)-1, (short)-1, (short)-1, (short)0, (short)0, (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessDatabaseSpecifications");

            migrationBuilder.DropTable(
                name: "DriverConfigurations");

            migrationBuilder.DropTable(
                name: "ScpDeviceSpecifications");

            migrationBuilder.DropTable(
                name: "Scps");

            migrationBuilder.DropTable(
                name: "SioPanelConfigurations");

            migrationBuilder.DropTable(
                name: "WriterAudits");
        }
    }
}
