using CandyShop.Models;

namespace CandyShop
{
    internal class DataSeed
    {
        string[] candyNames = { "Rainbow Lollipops", "Cotton Candy Clouds", "Choco-Caramel Delights", "Gummy Bear Bonanza",

    "Minty Chocolate Truffles", "Jellybean Jamboree", "Fruity Taffy Twists", "Sour Patch Surprise",

    "Crispy Peanut Butter Cups", "Rock Candy Crystals"};


        internal static void SeedData()
        {
            List<Product> products = new List<Product>
            {
                new ChocolateBar(1) { Name = "Milk Chocolate Bar", Price = 1.99m, CocoaPercentage = 30 },
                new ChocolateBar(2) { Name = "Dark Chocolate Bar", Price = 2.49m, CocoaPercentage = 70 },
                new ChocolateBar(3) { Name = "White Chocolate Bar", Price = 2.19m, CocoaPercentage = 0 },
                new ChocolateBar(4) { Name = "Hazelnut Chocolate Bar", Price = 2.79m, CocoaPercentage = 60 },
                new Lolipop(5) { Name = "Strawberry Swirl Lollipop", Price = 0.99m, Shape = "Round" },
                new Lolipop(6) { Name = "Watermelon Lollipop", Price = 1.29m, Shape = "Heart" },
                new Lolipop(7) { Name = "Grape Lollipop", Price = 0.89m, Shape = "Oval" },
                new Lolipop(8) { Name = "Blue Raspberry Lollipop", Price = 1.19m, Shape = "Star" },
                new ChocolateBar(9) { Name = "Mint Chocolate Bar", Price = 2.59m, CocoaPercentage = 50 },
                new ChocolateBar(10) { Name = "Almond Chocolate Bar", Price = 2.89m, CocoaPercentage = 55 }
            };

            var productsController = new ProductsController();

            productsController.AddProducts(products);

        }
    }
}
