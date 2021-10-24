using System;
using System.Collections.Generic;
using System.Threading;

namespace OnlineShop

// Shop class that handles all operations such as (re)stocking and removal of products
{
    public class Shop
    {
        private const int DEFAULT_SELLING_CONCURRENCY = 100;
        private const int DEFAULT_STOCKING_CONCURRENCY = 5;
        
        private Dictionary<Product, double> _products;
        private object _lock;

        private int orderConcurrency = 0;
        private int stockingConcurrency = 0;
        
        public Shop() :                                                     
            this(DEFAULT_SELLING_CONCURRENCY, DEFAULT_STOCKING_CONCURRENCY) { }
        
        public Shop(int orderConcurrency, int stockingConcurrency)
        {
            this._products = new Dictionary<Product, double>();
            this._lock = new object();
            this.orderConcurrency = orderConcurrency;
            this.stockingConcurrency = stockingConcurrency;
        }
        
        public void Stock(Dictionary<Product, double> products)
        {
            var threads = new Thread[stockingConcurrency];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    foreach (var nextDelivery in products)
                    {
                        this.AddProduct(nextDelivery.Key, nextDelivery.Value);
                    }
                });
                
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        public void Sell(Dictionary<Product, double> products)
        {
            var threads = new Thread[orderConcurrency];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    foreach (var nextDelivery in products)
                    {
                        this.RemoveProduct(nextDelivery.Key, nextDelivery.Value);
                    }
                });
                
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
        
        private void AddProduct(Product product, double quantity)
        { 
            this.AddOrUpdateProduct(product, quantity);
        }
        
        private void RemoveProduct(Product product, double quantity)
        {
            this.AddOrUpdateProduct(product, -quantity);
        }
        
        private void AddOrUpdateProduct(Product product, double quantity)
        {
            lock (this._lock)
            {
                if (!this._products.ContainsKey(product))
                {
                    this._products[product] = 0;
                }
                
                var value = this._products[product];

                if (value + quantity < 0)
                {
                    throw new ArgumentException($"Not enough quantity {quantity} for product {product}.");
                }

                this._products[product] = value + quantity;
            }
        }
        
        public Dictionary<Product, double> GetProducts()
        {
            lock (this._products)
            {
                var copy = new Dictionary<Product, double>(_products);
                return copy;
            }
        }
    }
}