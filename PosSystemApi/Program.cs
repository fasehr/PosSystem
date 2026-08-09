using PosSystemApi.Helpers;
using PosSystemApi.Services;
// Application entry point/ Application builder.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<ICheckoutService, CheckoutService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Load Products.csv when the application starts
using (var scope = app.Services.CreateScope())
{
    var productService = scope.ServiceProvider
        .GetRequiredService<IProductService>();

    ProductLoader.LoadProducts(productService);
}

// Middlewares
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();