using System;
using System.Collections.Generic;

namespace OnlineShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Product, double> newStockedProducts = new Dictionary<Product, double>()
            {
                { new Product("Apples", "123456", 1.00), 400 },
                { new Product("Bananas", "1234567", 3.00), 200 },
                { new Product("Sugar", "112312", 4.50), 200 },
                { new Product("Brownies", "3442334", 7.00), 10 },
                { new Product("Cookies", "323", 5.00), 2 },
                { new Product("Water", "233233", 1.00), 100 },
                { new Product("Cucumber","22324",1.43), 2 },
            };

            Dictionary<Product, double> orders = new Dictionary<Product, double>()
            {
                { new Product("Apples", "123456", 1.00), 20 },
                { new Product("Sugar", "112312", 1.00), 1 },
            };

            Shop shop = new Shop();
            
            shop.Stock(newStockedProducts);
            shop.Sell(orders);
            
            foreach (var product in shop.GetProducts())
            {
                Console.WriteLine($"{product.Key}, Quantity: {product.Value}");
            }
        }
    }
}