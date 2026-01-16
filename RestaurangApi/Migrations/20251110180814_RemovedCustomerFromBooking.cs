using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturangDB_API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCustomerFromBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_FK_CustomerID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FK_CustomerID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FK_CustomerID",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerID",
                table: "Bookings",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "FK_CustomerID",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_CustomerID",
                table: "Bookings",
                column: "FK_CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_FK_CustomerID",
                table: "Bookings",
                column: "FK_CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
