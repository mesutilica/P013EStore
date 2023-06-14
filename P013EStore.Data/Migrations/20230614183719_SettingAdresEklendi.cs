using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P013EStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SettingAdresEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 6, 14, 21, 37, 19, 892, DateTimeKind.Local).AddTicks(1206), new Guid("a8dae8f3-a5f3-4594-847f-f09e899d76c6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 6, 14, 21, 34, 3, 400, DateTimeKind.Local).AddTicks(8187), new Guid("5cb09f96-83c5-49c0-8a64-c55ece562afd") });
        }
    }
}
