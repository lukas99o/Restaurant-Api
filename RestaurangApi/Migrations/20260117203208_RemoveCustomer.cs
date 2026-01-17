using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResturangDB_API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 2,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 3,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 5,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 6,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 7,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 9,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 11,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 12,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 13,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 15,
                column: "TableSeats",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 17,
                column: "TableSeats",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 18,
                column: "TableSeats",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 19,
                column: "TableSeats",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "lukas@hotmail.com", "Lukas", null, "1234567890" },
                    { 2, "emma@gmail.com", "Emma", null, "2345678901" },
                    { 3, "noah@yahoo.com", "Noah", null, "3456789012" },
                    { 4, "olivia@outlook.com", "Olivia", null, "4567890123" },
                    { 5, "liam@hotmail.com", "Liam", null, "5678901234" },
                    { 6, "ava@gmail.com", "Ava", null, "6789012345" },
                    { 7, "ethan@yahoo.com", "Ethan", null, "7890123456" },
                    { 8, "sophia@outlook.com", "Sophia", null, "8901234567" },
                    { 9, "james@hotmail.com", "James", null, "9012345678" },
                    { 10, "mia@gmail.com", "Mia", null, "0123456789" },
                    { 11, "admin@email.com", null, "admin", null }
                });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 2,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 3,
                column: "TableSeats",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 5,
                column: "TableSeats",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 6,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 7,
                column: "TableSeats",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 9,
                column: "TableSeats",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 11,
                column: "TableSeats",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 12,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 13,
                column: "TableSeats",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 15,
                column: "TableSeats",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 17,
                column: "TableSeats",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 18,
                column: "TableSeats",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableID",
                keyValue: 19,
                column: "TableSeats",
                value: 6);

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
    }
}
