using Microsoft.EntityFrameworkCore;
using ResturangDB_API.Migrations;
using ResturangDB_API.Models;
using static System.Net.WebRequestMethods;

namespace ResturangDB_API.Data
{
    public class ResturangContext : DbContext
    {
        public ResturangContext(DbContextOptions<ResturangContext> options) : base(options) { }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Table>().HasData
                (
                    new Table { TableID = 1, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 2, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 3, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 4, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 5, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 6, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 7, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 8, TableSeats = 2, IsAvailable = true },
                    new Table { TableID = 9, TableSeats = 4, IsAvailable = true },
                    new Table { TableID = 10, TableSeats = 4, IsAvailable = true },
                    new Table { TableID = 11, TableSeats = 4, IsAvailable = true },
                    new Table { TableID = 12, TableSeats = 4, IsAvailable = true },
                    new Table { TableID = 13, TableSeats = 4, IsAvailable = true },
                    new Table { TableID = 14, TableSeats = 4, IsAvailable = true },
                    new Table { TableID = 15, TableSeats = 8, IsAvailable = true },
                    new Table { TableID = 16, TableSeats = 8, IsAvailable = true },
                    new Table { TableID = 17, TableSeats = 8, IsAvailable = true },
                    new Table { TableID = 18, TableSeats = 8, IsAvailable = true },
                    new Table { TableID = 19, TableSeats = 8, IsAvailable = true },
                    new Table { TableID = 20, TableSeats = 8, IsAvailable = true }
                );

            modelBuilder.Entity<Menu>().HasData
                (
                    new Menu { MenuID = 1, Name = "Main Menu" },
                    new Menu { MenuID = 2, Name = "Drink Menu" }
                );

            modelBuilder.Entity<MenuItem>().HasData
                (
                // Main Menu
                new MenuItem { MenuItemID = 1, Name = "Bolognese", Price = 130, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://www.recipetineats.com/tachyon/2018/07/Spaghetti-Bolognese.jpg", Description = "Classic Italian pasta with a rich, meaty tomato sauce." },
                new MenuItem { MenuItemID = 2, Name = "Margherita Pizza", Price = 120, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://eu-central-1.linodeobjects.com/tasteline/2018/06/pizza-margherita-foto-kerstin-eriksson-original-2048x2048.jpg", Description = "Fresh pizza topped with mozzarella, basil, and tomato sauce." },
                new MenuItem { MenuItemID = 3, Name = "Caesar Salad", Price = 95, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://cdn.kronfagel.se/app/uploads/2019/07/Caesarsallad.jpg", Description = "Crisp romaine lettuce with Caesar dressing, croutons, and Parmesan." },
                new MenuItem { MenuItemID = 4, Name = "Grilled Chicken", Price = 150, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://www.cookinwithmima.com/wp-content/uploads/2021/06/Grilled-BBQ-Chicken.jpg", Description = "Juicy grilled chicken marinated in flavorful herbs and spices." },
                new MenuItem { MenuItemID = 5, Name = "Fish Tacos", Price = 110, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://www.allrecipes.com/thmb/_emMPu4gpcuCOoC0kfjRWIdHlmc=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/53729-fish-tacos-DDMFS-4x3-b5547c67c6f0432da06ad8f905e82c1e.jpg", Description = "Soft tacos filled with crispy fried fish and zesty slaw." },
                new MenuItem { MenuItemID = 6, Name = "BBQ Ribs", Price = 200, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://www.barossafinefoods.com.au/glide-cache/containers/main/2020_bff_porkribs_bbq_website-2.jpg/03d880f2ca84b83fdeb147548e7d9b12.jpg", Description = "Tender pork ribs glazed with smoky BBQ sauce, grilled to perfection." },
                new MenuItem { MenuItemID = 7, Name = "Beef Stroganoff", Price = 160, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://www.recipetineats.com/tachyon/2018/01/Beef-Stroganoff_2-1-1.jpg", Description = "Creamy beef stroganoff served with sautéed mushrooms and pasta." },
                new MenuItem { MenuItemID = 8, Name = "Vegetable Stir-Fry", Price = 100, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://natashaskitchen.com/wp-content/uploads/2020/08/Vegetable-Stir-Fry-2.jpg", Description = "Fresh veggies stir-fried in a light soy sauce with ginger and garlic." },
                new MenuItem { MenuItemID = 9, Name = "Spaghetti Carbonara", Price = 135, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://static01.nyt.com/images/2021/02/14/dining/carbonara-horizontal/carbonara-horizontal-square640-v2.jpg", Description = "Classic Italian pasta with creamy egg sauce, pancetta, and Parmesan." },
                new MenuItem { MenuItemID = 10, Name = "Lasagna", Price = 140, IsAvailable = true, FK_MenuID = 1, ImgUrl = "https://thecozycook.com/wp-content/uploads/2022/04/Lasagna-Recipe-f.jpg", Description = "Layers of pasta, meat sauce, and cheese baked to golden perfection." },
                new MenuItem { MenuItemID = 11, Name = "Chicken Alfredo", Price = 145, IsAvailable = true, FK_MenuID = 1, Description = "Rich and creamy Alfredo sauce tossed with tender grilled chicken and pasta." },
                new MenuItem { MenuItemID = 12, Name = "Garlic Bread", Price = 50, IsAvailable = true, FK_MenuID = 1, Description = "Crispy bread slices toasted with garlic butter and fresh herbs." },
                new MenuItem { MenuItemID = 13, Name = "French Fries", Price = 40, IsAvailable = true, FK_MenuID = 1, Description = "Golden and crispy fries seasoned to perfection." },
                new MenuItem { MenuItemID = 14, Name = "Cheeseburger", Price = 110, IsAvailable = true, FK_MenuID = 1, Description = "Juicy beef patty topped with melted cheese, lettuce, and tomato." },
                new MenuItem { MenuItemID = 15, Name = "Greek Salad", Price = 85, IsAvailable = true, FK_MenuID = 1, Description = "Fresh veggies, feta cheese, and olives tossed in a light vinaigrette." },
                new MenuItem { MenuItemID = 16, Name = "Mushroom Risotto", Price = 130, IsAvailable = true, FK_MenuID = 1, Description = "Creamy Arborio rice cooked with sautéed mushrooms and Parmesan." },
                new MenuItem { MenuItemID = 17, Name = "Tuna Sandwich", Price = 75, IsAvailable = true, FK_MenuID = 1, Description = "Classic tuna salad sandwich served on toasted bread." },
                new MenuItem { MenuItemID = 18, Name = "Pancakes", Price = 90, IsAvailable = true, FK_MenuID = 1, Description = "Fluffy pancakes served with syrup and butter." },
                new MenuItem { MenuItemID = 19, Name = "Chocolate Cake", Price = 60, IsAvailable = true, FK_MenuID = 1, Description = "Decadent chocolate cake topped with rich frosting." },
                new MenuItem { MenuItemID = 20, Name = "Ice Cream Sundae", Price = 70, IsAvailable = true, FK_MenuID = 1, Description = "Creamy ice cream topped with chocolate sauce, nuts, and a cherry." },

                // Drink Menu
                new MenuItem { MenuItemID = 21, Name = "Coca-Cola", Price = 25, IsAvailable = true, FK_MenuID = 2, Description = "Refreshing carbonated soft drink." },
                new MenuItem { MenuItemID = 22, Name = "Sprite", Price = 25, IsAvailable = true, FK_MenuID = 2, Description = "Crisp lemon-lime flavored soda." },
                new MenuItem { MenuItemID = 23, Name = "Fanta", Price = 25, IsAvailable = true, FK_MenuID = 2, Description = "Fruity and sweet orange soda." },
                new MenuItem { MenuItemID = 24, Name = "Pepsi", Price = 25, IsAvailable = true, FK_MenuID = 2, Description = "Classic cola with a bold, refreshing taste." },
                new MenuItem { MenuItemID = 25, Name = "Iced Tea", Price = 30, IsAvailable = true, FK_MenuID = 2, Description = "Chilled tea served with lemon for a refreshing taste." },
                new MenuItem { MenuItemID = 26, Name = "Lemonade", Price = 30, IsAvailable = true, FK_MenuID = 2, Description = "Freshly squeezed lemonade with a tangy, sweet flavor." },
                new MenuItem { MenuItemID = 27, Name = "Orange Juice", Price = 35, IsAvailable = true, FK_MenuID = 2, Description = "Freshly squeezed orange juice, packed with vitamin C." },
                new MenuItem { MenuItemID = 28, Name = "Apple Juice", Price = 35, IsAvailable = true, FK_MenuID = 2, Description = "Crisp and sweet apple juice." },
                new MenuItem { MenuItemID = 29, Name = "Mineral Water", Price = 20, IsAvailable = true, FK_MenuID = 2, Description = "Refreshing and clean mineral water." },
                new MenuItem { MenuItemID = 30, Name = "Sparkling Water", Price = 25, IsAvailable = true, FK_MenuID = 2, Description = "Bubbly, refreshing sparkling water." }
                );
        }
    }
}
