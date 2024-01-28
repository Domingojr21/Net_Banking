using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBanking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDebt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Debt",
                table: "CreditCards",
                type: "Decimal(16,2)",
                precision: 16,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 11, 18, 18, 39, 3, 187, DateTimeKind.Local).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 11, 18, 18, 39, 3, 187, DateTimeKind.Local).AddTicks(2062));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 11, 18, 18, 39, 3, 187, DateTimeKind.Local).AddTicks(2063));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 11, 18, 18, 39, 3, 187, DateTimeKind.Local).AddTicks(2064));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 11, 18, 18, 39, 3, 187, DateTimeKind.Local).AddTicks(2066));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 11, 18, 18, 39, 3, 187, DateTimeKind.Local).AddTicks(2067));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debt",
                table: "CreditCards");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8940));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8963));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8965));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8973));
        }
    }
}
