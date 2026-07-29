using PosSystem.Models;
using PosSystem.Services;

namespace PosSystem.UI
{
    public static class MenuUI
    {
        public static void Start(
            ProductService productService,
            CartService cartService,
            CheckoutService checkoutService)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                DisplayMainMenu();

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        StoreManagementMenu(productService);
                        break;

                    case "2":
                        ShoppingCartMenu(productService, cartService);
                        break;

                    case "3":
                        CheckoutMenu(cartService, checkoutService);
                        break;

                    case "4":
                        OrderHistoryMenu(checkoutService);
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("\nThank you for using the POS System.");
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private static void DisplayMainMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("              POS SYSTEM");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Store Management");
            Console.WriteLine("2. Shopping Cart");
            Console.WriteLine("3. Checkout");
            Console.WriteLine("4. Order History");
            Console.WriteLine("5. Exit");
            Console.WriteLine("======================================");
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void StoreManagementMenu(ProductService productService)
        {
            bool goBack = false;

            while (!goBack)
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("         STORE MANAGEMENT");
                Console.WriteLine("======================================");
                Console.WriteLine("1. View Inventory");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Search Product");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Back");
                Console.WriteLine("======================================");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        ViewInventory(productService);
                        break;

                    case "2":
                        AddProduct(productService);
                        break;

                    case "3":
                        SearchProduct(productService);
                        break;

                    case "4":
                        RemoveProduct(productService);
                        break;

                    case "5":
                        goBack = true;
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private static void ViewInventory(ProductService productService)
        {
            Console.Clear();

            Console.WriteLine("============================================================================");
            Console.WriteLine("                                INVENTORY");
            Console.WriteLine("============================================================================");

            IReadOnlyList<Product> products = productService.GetAllProducts();

            if (products.Count == 0)
            {
                Console.WriteLine("\nNo products are available.");
                Pause();
                return;
            }

            Console.WriteLine(
                $"{"SKU",-12} {"Name",-25} {"Category",-18} {"Price",10} {"Stock",8}");

            Console.WriteLine(new string('-', 76));

            foreach (Product product in products)
            {
                Console.WriteLine(
                    $"{product.Sku,-12} " +
                    $"{product.Name,-25} " +
                    $"{product.Category,-18} " +
                    $"{product.UnitPrice,10:N2} " +
                    $"{product.StockQuantity,8}");
            }

            Pause();
        }

        private static void AddProduct(ProductService productService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("             ADD PRODUCT");
            Console.WriteLine("======================================");

            try
            {
                Console.Write("Enter SKU: ");
                string sku = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(sku))
                {
                    Console.WriteLine("\nSKU cannot be empty.");
                    Pause();
                    return;
                }

                Product? existingProduct =
                    productService.FindProductBySku(sku);

                if (existingProduct != null)
                {
                    Console.WriteLine("\nA product with this SKU already exists.");
                    Pause();
                    return;
                }

                Console.Write("Enter product name: ");
                string name = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter category: ");
                string category = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter unit price: ");
                bool validPrice = decimal.TryParse(
                    Console.ReadLine(),
                    out decimal unitPrice);

                if (!validPrice)
                {
                    Console.WriteLine("\nInvalid price.");
                    Pause();
                    return;
                }

                Console.Write("Enter stock quantity: ");
                bool validStock = int.TryParse(
                    Console.ReadLine(),
                    out int stockQuantity);

                if (!validStock)
                {
                    Console.WriteLine("\nInvalid stock quantity.");
                    Pause();
                    return;
                }

                Product product = new Product(sku,name,unitPrice,category,stockQuantity);

                productService.AddProduct(product);

                Console.WriteLine("\nProduct added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Pause();
        }

        private static void SearchProduct(ProductService productService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("           SEARCH PRODUCT");
            Console.WriteLine("======================================");

            Console.Write("Enter Product SKU: ");
            string sku = Console.ReadLine() ?? string.Empty;

            Product? product = productService.FindProductBySku(sku);

            if (product == null)
            {
                Console.WriteLine("\nProduct not found.");
            }
            else
            {
                Console.WriteLine("\n========== PRODUCT DETAILS ==========");
                Console.WriteLine($"SKU       : {product.Sku}");
                Console.WriteLine($"Name      : {product.Name}");
                Console.WriteLine($"Category  : {product.Category}");
                Console.WriteLine($"Price     : £{product.UnitPrice:N2}");
                Console.WriteLine($"Stock     : {product.StockQuantity}");
                Console.WriteLine("=====================================");
            }

            Pause();
        }

        private static void RemoveProduct(ProductService productService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("           REMOVE PRODUCT");
            Console.WriteLine("======================================");

            Console.Write("Enter Product SKU: ");
            string sku = Console.ReadLine() ?? string.Empty;

            Product? product = productService.FindProductBySku(sku);

            if (product == null)
            {
                Console.WriteLine("\nProduct not found.");
                Pause();
                return;
            }

            Console.WriteLine("\nProduct Details:");
            Console.WriteLine($"SKU       : {product.Sku}");
            Console.WriteLine($"Name      : {product.Name}");
            Console.WriteLine($"Category  : {product.Category}");
            Console.WriteLine($"Price     : £{product.UnitPrice:N2}");
            Console.WriteLine($"Stock     : {product.StockQuantity}");

            Console.Write("\nAre you sure you want to remove this product? (Y/N): ");
            string choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                productService.RemoveProduct(sku);
                Console.WriteLine("\nProduct removed successfully.");
            }
            else
            {
                Console.WriteLine("\nProduct removal cancelled.");
            }

            Pause();
        }

        private static void ShoppingCartMenu(
    ProductService productService,
    CartService cartService)
        {
            bool goBack = false;

            while (!goBack)
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("            SHOPPING CART");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Add Product to Cart");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Remove Product from Cart");
                Console.WriteLine("5. Clear Cart");
                Console.WriteLine("6. Back");
                Console.WriteLine("======================================");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        AddProductToCart(productService, cartService);
                        break;

                    case "2":
                        ViewCart(cartService);
                        break;

                    case "3":
                        UpdateCartQuantity(cartService);
                        break;

                    case "4":
                        RemoveProductFromCart(cartService);
                        break;

                    case "5":
                        ClearCart(cartService);
                        break;

                    case "6":
                        goBack = true;
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice.");
                        Pause();
                        break;
                }
            }
        }
        private static void AddProductToCart(
    ProductService productService,
    CartService cartService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("        ADD PRODUCT TO CART");
            Console.WriteLine("======================================");

            Console.Write("Enter Product SKU: ");
            string sku = Console.ReadLine() ?? string.Empty;

            Product? product = productService.FindProductBySku(sku);

            if (product == null)
            {
                Console.WriteLine("\nProduct not found.");
                Pause();
                return;
            }

            Console.WriteLine($"\nProduct: {product.Name}");
            Console.WriteLine($"Price: £{product.UnitPrice:N2}");
            Console.WriteLine($"Available Stock: {product.StockQuantity}");

            Console.Write("\nEnter quantity: ");

            bool validQuantity = int.TryParse(
                Console.ReadLine(),
                out int quantity);

            if (!validQuantity)
            {
                Console.WriteLine("\nInvalid quantity.");
                Pause();
                return;
            }

            try
            {
                cartService.AddToCart(product, quantity);

                Console.WriteLine(
                    $"\n{product.Name} added to the cart successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Pause();
        }

        private static void ViewCart(CartService cartService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("             SHOPPING CART");
            Console.WriteLine("======================================");

            Cart cart = cartService.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("\nYour cart is empty.");
                Pause();
                return;
            }

            Console.WriteLine(
                $"{"SKU",-12} {"Name",-25} {"Qty",5} {"Price",10} {"Total",10}");

            Console.WriteLine(new string('-', 70));

            foreach (CartItem item in cart.Items)
            {
                Console.WriteLine(
                    $"{item.Product.Sku,-12} " +
                    $"{item.Product.Name,-25} " +
                    $"{item.Quantity,5} " +
                    $"{item.Product.UnitPrice,10:N2} " +
                    $"{item.LineTotal,10:N2}");
            }

            Console.WriteLine(new string('-', 70));
            Console.WriteLine($"Cart Total: £{cart.Total:N2}");

            Pause();
        }
        private static void UpdateCartQuantity(CartService cartService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("          UPDATE QUANTITY");
            Console.WriteLine("======================================");

            Cart cart = cartService.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("\nYour cart is empty.");
                Pause();
                return;
            }

            Console.Write("Enter Product SKU: ");
            string sku = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter new quantity: ");

            bool validQuantity = int.TryParse(
                Console.ReadLine(),
                out int quantity);

            if (!validQuantity)
            {
                Console.WriteLine("\nInvalid quantity.");
                Pause();
                return;
            }

            try
            {
                cartService.UpdateQuantity(sku, quantity);

                Console.WriteLine("\nQuantity updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Pause();
        }

        private static void RemoveProductFromCart(CartService cartService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("      REMOVE PRODUCT FROM CART");
            Console.WriteLine("======================================");

            Cart cart = cartService.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("\nYour cart is empty.");
                Pause();
                return;
            }

            Console.Write("Enter Product SKU: ");
            string sku = Console.ReadLine() ?? string.Empty;

            try
            {
                cartService.RemoveFromCart(sku);

                Console.WriteLine("\nProduct removed from the cart successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Pause();
        }

        private static void ClearCart(CartService cartService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("             CLEAR CART");
            Console.WriteLine("======================================");

            Cart cart = cartService.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("\nYour cart is already empty.");
                Pause();
                return;
            }

            Console.Write("Are you sure you want to clear the cart? (Y/N): ");
            string choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                cartService.ClearCart();
                Console.WriteLine("\nCart cleared successfully.");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }

            Pause();
        }
        private static void CheckoutMenu(
    CartService cartService,
    CheckoutService checkoutService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("               CHECKOUT");
            Console.WriteLine("======================================");

            Cart cart = cartService.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("\nYour cart is empty.");
                Pause();
                return;
            }

            Console.WriteLine("\nOrder Summary:");
            Console.WriteLine("--------------------------------------");

            foreach (CartItem item in cart.Items)
            {
                Console.WriteLine(
                    $"{item.Product.Name} x {item.Quantity} = " +
                    $"£{item.LineTotal:N2}");
            }

            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Total: £{cart.Total:N2}");

            Console.Write("\nConfirm checkout? (Y/N): ");
            string choice = Console.ReadLine() ?? string.Empty;

            if (!choice.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nCheckout cancelled.");
                Pause();
                return;
            }

            try
            {
                Order order = checkoutService.Checkout(cart);

                Console.WriteLine("\nCheckout completed successfully.");
                Console.WriteLine($"Order ID: {order.OrderId}");

                checkoutService.DisplayReceipt(order);
                checkoutService.DisplayOrderAsJson(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Pause();
        }
        private static void OrderHistoryMenu(
    CheckoutService checkoutService)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("            ORDER HISTORY");
            Console.WriteLine("======================================");

            var orders = checkoutService.GetOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("\nNo orders have been placed.");
                Pause();
                return;
            }

            foreach (Order order in orders)
            {
                Console.WriteLine($"\nOrder ID : {order.OrderId}");
                Console.WriteLine($"Date     : {order.CreatedAt}");
                Console.WriteLine("--------------------------------------");

                foreach (CartItem item in order.Items)
                {
                    Console.WriteLine(
                        $"{item.Product.Name} x {item.Quantity} = £{item.LineTotal:N2}");
                }

                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"Order Total: £{order.Total:N2}");
            }

            Pause();
        }
    }
}