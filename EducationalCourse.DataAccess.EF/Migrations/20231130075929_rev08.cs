using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalCourse.DataAccess.EF.Migrations
{
    /// <inheritdoc />
    public partial class rev08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditAmount",
                table: "WalletTransactions");

            migrationBuilder.RenameColumn(
                name: "DepositAmount",
                table: "WalletTransactions",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "WalletTransactions",
                newName: "DepositAmount");

            migrationBuilder.AddColumn<int>(
                name: "CreditAmount",
                table: "WalletTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
