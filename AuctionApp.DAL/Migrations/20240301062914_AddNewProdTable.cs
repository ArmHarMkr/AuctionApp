using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNewProdTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "AuctionProducts",
                newName: "AuctionProdId");

            migrationBuilder.AlterColumn<double>(
                name: "LastPrice",
                table: "AuctionProducts",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "LastBidPrice",
                table: "AuctionProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuctionProdId",
                table: "AuctionProducts",
                newName: "ProductId");

            migrationBuilder.AlterColumn<double>(
                name: "LastPrice",
                table: "AuctionProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LastBidPrice",
                table: "AuctionProducts",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
