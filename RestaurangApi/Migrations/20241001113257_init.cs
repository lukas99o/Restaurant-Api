using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResturangDB_API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableSeats = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableID);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    FK_MenuID = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemID);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_FK_MenuID",
                        column: x => x.FK_MenuID,
                        principalTable: "Menus",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_CustomerID = table.Column<int>(type: "int", nullable: false),
                    FK_TableID = table.Column<int>(type: "int", nullable: false),
                    AmountOfPeople = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_FK_CustomerID",
                        column: x => x.FK_CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_FK_TableID",
                        column: x => x.FK_TableID,
                        principalTable: "Tables",
                        principalColumn: "TableID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "lukas@hotmail.com", "Lukas", "1234567890" },
                    { 2, "emma@gmail.com", "Emma", "2345678901" },
                    { 3, "noah@yahoo.com", "Noah", "3456789012" },
                    { 4, "olivia@outlook.com", "Olivia", "4567890123" },
                    { 5, "liam@hotmail.com", "Liam", "5678901234" },
                    { 6, "ava@gmail.com", "Ava", "6789012345" },
                    { 7, "ethan@yahoo.com", "Ethan", "7890123456" },
                    { 8, "sophia@outlook.com", "Sophia", "8901234567" },
                    { 9, "james@hotmail.com", "James", "9012345678" },
                    { 10, "mia@gmail.com", "Mia", "0123456789" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "Name" },
                values: new object[,]
                {
                    { 1, "Main Menu" },
                    { 2, "Drink Menu" }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableID", "IsAvailable", "TableSeats" },
                values: new object[,]
                {
                    { 1, true, 2 },
                    { 2, true, 4 },
                    { 3, true, 6 },
                    { 4, true, 2 },
                    { 5, true, 8 },
                    { 6, true, 4 },
                    { 7, true, 10 },
                    { 8, true, 2 },
                    { 9, true, 6 },
                    { 10, true, 4 },
                    { 11, true, 8 },
                    { 12, true, 2 },
                    { 13, true, 10 },
                    { 14, true, 4 },
                    { 15, true, 6 },
                    { 16, true, 8 },
                    { 17, true, 2 },
                    { 18, true, 4 },
                    { 19, true, 6 },
                    { 20, true, 8 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemID", "Description", "FK_MenuID", "ImgUrl", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Classic Italian pasta with a rich, meaty tomato sauce.", 1, "https://www.recipetineats.com/tachyon/2018/07/Spaghetti-Bolognese.jpg", true, "Bolognese", 130 },
                    { 2, "Fresh pizza topped with mozzarella, basil, and tomato sauce.", 1, "https://eu-central-1.linodeobjects.com/tasteline/2018/06/pizza-margherita-foto-kerstin-eriksson-original-2048x2048.jpg", true, "Margherita Pizza", 120 },
                    { 3, "Crisp romaine lettuce with Caesar dressing, croutons, and Parmesan.", 1, "https://cdn.kronfagel.se/app/uploads/2019/07/Caesarsallad.jpg", true, "Caesar Salad", 95 },
                    { 4, "Juicy grilled chicken marinated in flavorful herbs and spices.", 1, "https://www.cookinwithmima.com/wp-content/uploads/2021/06/Grilled-BBQ-Chicken.jpg", true, "Grilled Chicken", 150 },
                    { 5, "Soft tacos filled with crispy fried fish and zesty slaw.", 1, "https://www.allrecipes.com/thmb/_emMPu4gpcuCOoC0kfjRWIdHlmc=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/53729-fish-tacos-DDMFS-4x3-b5547c67c6f0432da06ad8f905e82c1e.jpg", true, "Fish Tacos", 110 },
                    { 6, "Tender pork ribs glazed with smoky BBQ sauce, grilled to perfection.", 1, "https://www.barossafinefoods.com.au/glide-cache/containers/main/2020_bff_porkribs_bbq_website-2.jpg/03d880f2ca84b83fdeb147548e7d9b12.jpg", true, "BBQ Ribs", 200 },
                    { 7, "Creamy beef stroganoff served with sautéed mushrooms and pasta.", 1, "https://www.recipetineats.com/tachyon/2018/01/Beef-Stroganoff_2-1-1.jpg", true, "Beef Stroganoff", 160 },
                    { 8, "Fresh veggies stir-fried in a light soy sauce with ginger and garlic.", 1, "https://natashaskitchen.com/wp-content/uploads/2020/08/Vegetable-Stir-Fry-2.jpg", true, "Vegetable Stir-Fry", 100 },
                    { 9, "Classic Italian pasta with creamy egg sauce, pancetta, and Parmesan.", 1, "https://static01.nyt.com/images/2021/02/14/dining/carbonara-horizontal/carbonara-horizontal-square640-v2.jpg", true, "Spaghetti Carbonara", 135 },
                    { 10, "Layers of pasta, meat sauce, and cheese baked to golden perfection.", 1, "https://thecozycook.com/wp-content/uploads/2022/04/Lasagna-Recipe-f.jpg", true, "Lasagna", 140 },
                    { 11, "Rich and creamy Alfredo sauce tossed with tender grilled chicken and pasta.", 1, null, true, "Chicken Alfredo", 145 },
                    { 12, "Crispy bread slices toasted with garlic butter and fresh herbs.", 1, null, true, "Garlic Bread", 50 },
                    { 13, "Golden and crispy fries seasoned to perfection.", 1, null, true, "French Fries", 40 },
                    { 14, "Juicy beef patty topped with melted cheese, lettuce, and tomato.", 1, null, true, "Cheeseburger", 110 },
                    { 15, "Fresh veggies, feta cheese, and olives tossed in a light vinaigrette.", 1, null, true, "Greek Salad", 85 },
                    { 16, "Creamy Arborio rice cooked with sautéed mushrooms and Parmesan.", 1, null, true, "Mushroom Risotto", 130 },
                    { 17, "Classic tuna salad sandwich served on toasted bread.", 1, null, true, "Tuna Sandwich", 75 },
                    { 18, "Fluffy pancakes served with syrup and butter.", 1, null, true, "Pancakes", 90 },
                    { 19, "Decadent chocolate cake topped with rich frosting.", 1, null, true, "Chocolate Cake", 60 },
                    { 20, "Creamy ice cream topped with chocolate sauce, nuts, and a cherry.", 1, null, true, "Ice Cream Sundae", 70 },
                    { 21, "Refreshing carbonated soft drink.", 2, null, true, "Coca-Cola", 25 },
                    { 22, "Crisp lemon-lime flavored soda.", 2, null, true, "Sprite", 25 },
                    { 23, "Fruity and sweet orange soda.", 2, null, true, "Fanta", 25 },
                    { 24, "Classic cola with a bold, refreshing taste.", 2, null, true, "Pepsi", 25 },
                    { 25, "Chilled tea served with lemon for a refreshing taste.", 2, null, true, "Iced Tea", 30 },
                    { 26, "Freshly squeezed lemonade with a tangy, sweet flavor.", 2, null, true, "Lemonade", 30 },
                    { 27, "Freshly squeezed orange juice, packed with vitamin C.", 2, null, true, "Orange Juice", 35 },
                    { 28, "Crisp and sweet apple juice.", 2, null, true, "Apple Juice", 35 },
                    { 29, "Refreshing and clean mineral water.", 2, null, true, "Mineral Water", 20 },
                    { 30, "Bubbly, refreshing sparkling water.", 2, null, true, "Sparkling Water", 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_CustomerID",
                table: "Bookings",
                column: "FK_CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_TableID",
                table: "Bookings",
                column: "FK_TableID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_FK_MenuID",
                table: "MenuItems",
                column: "FK_MenuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
