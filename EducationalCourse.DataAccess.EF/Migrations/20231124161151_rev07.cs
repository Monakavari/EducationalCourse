using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalCourse.DataAccess.EF.Migrations
{
    /// <inheritdoc />
    public partial class rev07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletType",
                table: "Wallets");

            migrationBuilder.AddColumn<int>(
                name: "WalletType",
                table: "WalletTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletType",
                table: "WalletTransactions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Wallets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WalletType",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
