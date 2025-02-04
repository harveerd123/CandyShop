using Microsoft.Data.Sqlite;
using static CandyShop.Enums;

namespace CandyShop.Models
{
    public abstract class Product
    {
        public int Id { get; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public Product(int id)
        {
            Id = id;
        }

        public Product()
        {

        }

        public abstract string[] GetColumnsArray(Product product);
        public abstract string GetProductForPanel();

        public abstract string GetInsertQuery();

        public abstract string GetUpdateQuery();

        public abstract void AddParameters(SqliteCommand cmd);
    }

    public class ChocolateBar : Product
    {
        public int CocoaPercentage { get; set; }
        public ChocolateBar(int id) : base(id)
        {
            Type = ProductType.ChocolateBar;
        }

        public ChocolateBar()
        {
            Type = ProductType.ChocolateBar;
        }

        public override string[] GetColumnsArray(Product product)
        {
            return new string[]
            {
                Id.ToString(),
                Type.ToString(),
                Name,
                Price.ToString(),
                CocoaPercentage.ToString()
            };
        }

        public override string GetProductForPanel()
        {
            return $@"Id: {Id}
Type: {Type}
Name: {Name}
Cocoa Percentage: {CocoaPercentage}";
        }

        public override string GetInsertQuery()
        {
            return $@"INSERT INTO products (name, price, type, cocoaPercentage) VALUES (@Name, @Price, @Type,
                @CocoaPercentage)";
        }

        public override void AddParameters(SqliteCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@Type", (int)Type);
            cmd.Parameters.AddWithValue("@CocoaPercentage", CocoaPercentage);

        }

        public override string GetUpdateQuery()
        {
            return $"UPDATE products SET name = @Name, price = @Price, type = 0, cocoapercentage = @CocoaPercentage WHERE Id = {Id}";
        }
    }

    public class Lolipop : Product
    {
        public string Shape { get; set; }
        public Lolipop(int id) : base(id)
        {
            Type = ProductType.Lolipop;
        }

        public Lolipop()
        {
            Type = ProductType.Lolipop;
        }

        public override string[] GetColumnsArray(Product product)
        {
            return new string[]
            {
                Id.ToString(),
                Type.ToString(),
                Name,
                Price.ToString(),
                "",
                Shape
            };
        }

        public override string GetProductForPanel()
        {
            return $@"Id: {Id}
Type: {Type}
Name: {Name}
Shape: {Shape}";
        }

        public override string GetInsertQuery()
        {
            return $@"INSERT INTO products (name, price, type, shape) VALUES (@Name, @Price, @Type,
                @Shape)";
        }


        public override void AddParameters(SqliteCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@Type", (int)Type);
            cmd.Parameters.AddWithValue("@Shape", Shape);

        }

        public override string GetUpdateQuery()
        {
            return $"UPDATE products SET name = @Name, price = @Price, type = 1, shape = @Shape WHERE Id = {Id}";
        }
    }

}
