using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleAtm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pin",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "BankAccounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Balance",
                table: "BankAccounts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "AccountNumber",
                table: "BankAccounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Pin",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
