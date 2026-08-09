using PosSystemApi.Models;
using PosSystemApi.Services;

namespace PosSystemApi.Helpers
{
    public static class ProductLoader
    {
        public static void LoadProducts(
            IProductService productService)
        {
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "Products.csv");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Products.csv not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(',');

                    Product product = new Product(
                        data[0],
                        data[1],
                        decimal.Parse(data[2]),
                        data[3],
                        int.Parse(data[4]));

                    productService.AddProduct(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid record: {line}");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}