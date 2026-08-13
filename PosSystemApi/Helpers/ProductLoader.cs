using PosSystemApi.Models;
using PosSystemApi.Services;

namespace PosSystemApi.Helpers
{
    public static class ProductLoader
    {
        public static async Task LoadProductsAsync(
            IProductService productService,
            string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    "Products CSV file was not found.",
                    filePath);
            }

            string[] lines = await File.ReadAllLinesAsync(filePath);

            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] values = line.Split(',');

                if (values.Length < 5)
                {
                    continue;
                }

                string sku = values[0].Trim();
                string name = values[1].Trim();

                if (!decimal.TryParse(
                    values[2],
                    out decimal unitPrice))
                {
                    continue;
                }

                string category = values[3].Trim();

                if (!int.TryParse(
                    values[4],
                    out int stockQuantity))
                {
                    continue;
                }

                Product product = new Product(
                    sku,
                    name,
                    unitPrice,
                    category,
                    stockQuantity);

                Product? existingProduct =
                    await productService.FindProductBySkuAsync(sku);

                if (existingProduct == null)
                {
                    await productService.AddProductAsync(product);
                }
            }
        }
    }
}