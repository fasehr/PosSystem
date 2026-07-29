using Newtonsoft.Json;
using PosSystem.Helpers;
using PosSystem.Models;
using PosSystem.Services;
using PosSystem.UI;



ProductService productService = new ProductService();
CartService cartService = new CartService();
CheckoutService checkoutService = new CheckoutService();

ProductLoader.LoadProducts(productService);

MenuUI.Start(
    productService,
    cartService,
    checkoutService);

