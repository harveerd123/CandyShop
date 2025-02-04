using CandyShop.Models;
using Microsoft.Data.Sqlite;
using static CandyShop.Enums;

namespace CandyShop
{

    public interface IProductsController
    {
        void CreateDatabase();

        List<Product> GetProducts();

        void AddProduct(Product product);

        void AddProducts(List<Product> product);

        void DeleteProduct(Product product);

        void UpdateProduct(Product product);


    }
    public class ProductsController: IProductsController
    {

        private string ConnectionString { get; } = "Data Source = products.db";

        public void CreateDatabase()
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                using var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = @"CREATE TABLE products (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        Price REAL NOT NULL,
                                        CocoaPercentage INTEGER NULL,
                                        Shape TEXT NULL,
                                        Type INTEGER NOT NULL
                                        )";
                tableCmd.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                using var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM products";

                using var reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(5) == (int)ProductType.ChocolateBar)
                        {
                            products.Add(new ChocolateBar(reader.GetInt32(0))
                            {
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                CocoaPercentage = reader.GetInt32(3)
                            });
                        }   else
                        {
                            products.Add(new Lolipop(reader.GetInt32(0))
                            {
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Shape = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(UserInterface.divide);
            }

            return products;
        }
        public void AddProduct(Product product)
        {


            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = product.GetInsertQuery();
                product.AddParameters(tableCmd);

                tableCmd.ExecuteNonQuery();
                Console.WriteLine("Product saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving product: " + ex.Message);
            }
        }

        public void AddProducts(List<Product> products)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(Configuration.docPath))
                {
                    outputFile.WriteLine("Id,Type,Name,Price,CocoaPercentage,Shape");
                    foreach (var product in products)
                    {
                        //var csvLine = product.GetProductForCsv(product.Id);
                        //outputFile.WriteLine(csvLine);
                    }
                }
                Console.WriteLine("Products saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error saving products: " + ex.Message);
            }
        }
        public void DeleteProduct(Product product)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString); 
                connection.Open();
                using var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE FROM products WHERE Id = {product.Id}";

                tableCmd.ExecuteNonQuery(); 
            }
            catch (SqliteException ex)
            {
                Console.WriteLine("There was an error deleting the product: " +ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = product.GetUpdateQuery();
                product.AddParameters(tableCmd);

                tableCmd.ExecuteNonQuery();
                Console.WriteLine("Product saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error updating product: " + ex.Message);
            }
        }
    }
}
