using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineOrderingSystem
{

    public class Address
    {
        private string StreetAddress { get; set; }
        private string City { get; set; }
        private string StateOrProvince { get; set; }
        private string Country { get; set; }

        public Address(string streetAddress, string city, string stateOrProvince, string country)
        {
            StreetAddress = streetAddress;
            City = city;
            StateOrProvince = stateOrProvince;
            Country = country;
        }

        public bool IsInUSA()
        {
            return Country.ToLower() == "usa";
        }

        public string GetFullAddress()
        {
            return $"{StreetAddress}\n{City}, {StateOrProvince}\n{Country}";
        }
    }

    public class Customer
    {
        private string Name { get; set; }
        private Address Address { get; set; }

        public Customer(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public bool IsInUSA()
        {
            return Address.IsInUSA();
        }

        public string GetName()
        {
            return Name;
        }

        public Address GetAddress()
        {
            return Address;
        }
    }

    public class Product
    {
        private string Name { get; set; }
        private string ProductId { get; set; }
        private decimal Price { get; set; }
        private int Quantity { get; set; }

        public Product(string name, string productId, decimal price, int quantity)
        {
            Name = name;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotalCost()
        {
            return Price * Quantity;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetProductId()
        {
            return ProductId;
        }
    }

    public class Order
    {
        private List<Product> Products { get; set; }
        private Customer Customer { get; set; }

        public Order(Customer customer)
        {
            Customer = customer;
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public decimal CalculateTotalCost()
        {
            decimal totalCost = 0;
            foreach (var product in Products)
            {
                totalCost += product.GetTotalCost();
            }
            totalCost += Customer.IsInUSA() ? 5 : 35;
            return totalCost;
        }

        public string GetPackingLabel()
        {
            StringBuilder packingLabel = new StringBuilder();
            foreach (var product in Products)
            {
                packingLabel.AppendLine($"{product.GetName()} (ID: {product.GetProductId()})");
            }
            return packingLabel.ToString();
        }

        public string GetShippingLabel()
        {
            StringBuilder shippingLabel = new StringBuilder();
            shippingLabel.AppendLine(Customer.GetName());
            shippingLabel.AppendLine(Customer.GetAddress().GetFullAddress());
            return shippingLabel.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Address address1 = new Address("5572 Jules Street", "Springfield", "IL", "USA");
            Address address2 = new Address("452 Genisis Avenue", "Toronto", "ON", "Canada");

            Customer customer1 = new Customer("James Fields", address1);
            Customer customer2 = new Customer("Kate Green", address2);

            Product product1 = new Product("Laptop", "LM153334P", 999.99m, 1);
            Product product2 = new Product("Mouse", "MO001928I", 19.99m, 1);
            Product product3 = new Product("Keyboard", "KT945783W", 49.99m, 1);

            Product product4 = new Product("Lawn Chair", "MN827483G", 199.99m, 2);
            Product product5 = new Product("Garden Shovel", "UC103964H", 9.99m, 1);
            Product product6 = new Product("Water Bottles", "GH955492B", 15.99m, 2);

            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);
            order1.AddProduct(product3);

            Order order2 = new Order(customer2);
            order2.AddProduct(product4);
            order2.AddProduct(product5);
            order2.AddProduct(product6);

            List<Order> orders = new List<Order> { order1, order2 };
            foreach (var order in orders)
            {
                Console.WriteLine("Packing Label:");
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine("Shipping Label:");
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Cost: {order.CalculateTotalCost():C}\n");
            }
        }
    }
}
