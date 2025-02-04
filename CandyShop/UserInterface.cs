using CandyShop.Models;
using Spectre.Console;
using System.Drawing;
using static CandyShop.Enums;

namespace CandyShop
{
    internal static class UserInterface
    {
        internal const string divide = "--------------------------";
        internal static void RunMainMenu()
        {

            var productsController = new ProductsController();
           
            var isMenuRunning = true;
            while (isMenuRunning)
            {
                PrintHeader();

                var usersChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuOptions>()
                    .AddChoices(MainMenuOptions.ViewProducts,
                    MainMenuOptions.ViewSingleProduct,
                    MainMenuOptions.AddProduct,
                    MainMenuOptions.UpdateProduct,
                    MainMenuOptions.DeleteProduct,
                    MainMenuOptions.QuitProgram));

                var menuMessage = "Press Any Key To Go Back to Menu";

                switch (usersChoice)
                {
                    case MainMenuOptions.AddProduct:
                        var product = GetProductInput();
                        productsController.AddProduct(product);
                        break;
                    case MainMenuOptions.DeleteProduct:
                        var productToDelete = GetProductChoice();
                        productsController.DeleteProduct(productToDelete);
                        break;
                    case MainMenuOptions.ViewProducts:
                        var products = productsController.GetProducts();
                        ViewProducts(products);
                        break;
                    case MainMenuOptions.ViewSingleProduct:
                        var productChoice = GetProductChoice();
                        ViewProduct(productChoice);
                        break;
                    case MainMenuOptions.UpdateProduct:
                        var productToUpdate = GetProductChoice();
                        var updatedProduct = GetProductUpdateInput(productToUpdate);
                        productsController.UpdateProduct(updatedProduct);
                        break;
                    case MainMenuOptions.QuitProgram:
                        menuMessage = "Goodbye!";
                        isMenuRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose one of the above");
                        break;
                }

                Console.WriteLine(menuMessage);
                Console.ReadLine();
                Console.Clear();
            }

        }

        private static Product GetProductUpdateInput(Product product)
        {
            Console.WriteLine("You'll be prompted with the choice to update each property. Press enter for Yes and N for no.");

            product.Name = AnsiConsole.Confirm("Update name?")  ? AnsiConsole.Ask<string>("Product's new name:") : product.Name;
            product.Price = AnsiConsole.Confirm("Update price?") ? AnsiConsole.Ask<decimal>("Product's new price:") : product.Price;

            var updateType = AnsiConsole.Confirm("Update category?");

            var type = ProductType.ChocolateBar;
            if (updateType)
            {
                type = AnsiConsole.Prompt(new SelectionPrompt<ProductType>()
                    .Title("Product Type:")
                    .AddChoices(
                    ProductType.ChocolateBar,
                    ProductType.Lolipop));
                if (type == ProductType.ChocolateBar) 
                {
                    Console.WriteLine("Cocoa %");
                    var cocoa = int.Parse(Console.ReadLine());


                    return new ChocolateBar(product.Id)
                    {
                        Name = product.Name,
                        Price = product.Price,
                        CocoaPercentage = cocoa
                    };
                }

                Console.WriteLine("Shape: ");
                var shape = Console.ReadLine();

                return new Lolipop(product.Id)
                {
                    Name = product.Name,
                    Price = product.Price,
                    Shape = shape
                };
            }

            return product;

        }

        private static void ViewProduct(Product productChoice)
        {
            var panel = new Panel(productChoice.GetProductForPanel());
            panel.Header = new PanelHeader("Product Info");
            panel.Padding = new Padding(2, 2, 2, 2);
            AnsiConsole.Write(panel);

            Console.WriteLine("Press Any Key to Return to Menu");
            Console.ReadLine();
            Console.Clear();
        }

        private static Product GetProductChoice()
        {
            var productsController = new ProductsController();
            var products = productsController.GetProducts();
            var productsArray = products.Select(x => x.Name).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose a product")
                .AddChoices(productsArray));

            var product = products.Single(x => x.Name == option);
            return product;
        }

        internal static void ViewProducts(List<Product> products)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Type");
            table.AddColumn("Name");
            table.AddColumn("Price");
            table.AddColumn("CocoaPercentage");
            table.AddColumn("Shape");

            foreach(var product in products) 
            {
                table.AddRow(product.GetColumnsArray(product));
            }

            AnsiConsole.Write(table);

        }

        internal static void PrintHeader()
        {
            var title = "Mary's Candy Shop";
            var dateTime = DateTime.Now;
            var daysSinceOpening = Helpers.GetDaysSinceOpening();
            var todaysProfit = 5.5m;
            var targetAchieved = false;

            Console.WriteLine(title);
            Console.WriteLine(divide);
            Console.WriteLine($"Today's date: {dateTime}");
            Console.WriteLine($"Days since opening: {daysSinceOpening}");
            Console.WriteLine($"Today's profit: {todaysProfit}$");
            Console.WriteLine($"Today's target achieved: {targetAchieved}");
            Console.WriteLine(divide);
        }

        private static Product GetProductInput()
        {
            Console.WriteLine("Product name:");
            var name = Console.ReadLine();
            while(Validation.IsStringValid(name) == false)
            {
                Console.WriteLine("Name cannot be empty or have more than 20 characters. Try again");
                name = Console.ReadLine();
            }    

            Console.WriteLine("Product price:");
            var priceInput = Console.ReadLine();
            var priceValidation = Validation.IsPriceValid(priceInput);

            while(!priceValidation.IsValid)
            {
                Console.WriteLine(priceValidation.ErrorMessage);
                priceInput = Console.ReadLine();
                priceValidation = Validation.IsPriceValid(priceInput);
            }

            var type = AnsiConsole.Prompt(
                new SelectionPrompt<ProductType>()
                .Title("Product type:")
                .AddChoices(
                    ProductType.Lolipop,
                    ProductType.ChocolateBar
                ));

            if (type == ProductType.ChocolateBar )
            {
                Console.WriteLine("Cocoa %");
                var cocoaInput = Console.ReadLine();
                var cocoaValidation = Validation.IsCocoaValid(cocoaInput);

                while(!cocoaValidation.IsValid)
                {
                    Console.WriteLine(cocoaValidation.ErrorMessage);    
                    cocoaInput = Console.ReadLine();
                    cocoaValidation = Validation.IsCocoaValid(cocoaInput);
                }
                return new ChocolateBar()
                {
                    Name = name,
                    Price = priceValidation.Price,
                    CocoaPercentage = cocoaValidation.CocoaPercentage
                };
            }

            Console.WriteLine("Shape:");
            var shape = Console.ReadLine();

            while (Validation.IsStringValid(shape) == false)
            {
                Console.WriteLine("Shape cannot be empty or have more than 20 characters. Try again");
                shape = Console.ReadLine();
            }

            return new Lolipop()
            {
                Name = name,
                Price = priceValidation.Price,
                Shape = shape
            };
        }
    }
}
