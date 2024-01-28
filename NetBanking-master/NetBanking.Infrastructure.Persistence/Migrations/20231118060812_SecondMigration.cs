using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBanking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8940), true });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8963), true });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8965), true });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8968), true });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8970), true });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 18, 2, 8, 10, 931, DateTimeKind.Local).AddTicks(8973), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 14, 18, 1, 25, 390, DateTimeKind.Local).AddTicks(7021), false });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 14, 18, 1, 25, 390, DateTimeKind.Local).AddTicks(7037), false });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 14, 18, 1, 25, 390, DateTimeKind.Local).AddTicks(7038), false });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 14, 18, 1, 25, 390, DateTimeKind.Local).AddTicks(7040), false });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 14, 18, 1, 25, 390, DateTimeKind.Local).AddTicks(7041), false });

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Status" },
                values: new object[] { new DateTime(2023, 11, 14, 18, 1, 25, 390, DateTimeKind.Local).AddTicks(7043), false });
        }
    }
}
