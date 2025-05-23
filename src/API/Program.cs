using System.Globalization;
using API.Config;
using Application.Commands.CreateItemListing;
using Application.Commands.CreateOrder;
using Application.Commands.CreateReview;
using Application.Commands.CreateUserProfile;
using Application.Interfaces;
using Application.Queries.GetItemListingById;
using Application.Queries.GetItemListings;
using Application.Queries.GetOrderById;
using Application.Queries.GetOrders;
using Application.Queries.GetReviews;
using Application.Queries.GetReviewsBySellerId;
using Application.Queries.GetUserProfiles;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Set culture info for the application
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
CultureInfo.CurrentCulture = cultureInfo;
CultureInfo.CurrentUICulture = cultureInfo;

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SecondHand E-Commerce API", Version = "v1" });
});

// Load configs
var mongoConfig = builder.Configuration.GetSection("Mongo").Get<MongoConfig>();
var redisConfig = builder.Configuration.GetSection("Redis").Get<RedisConfig>();
var minioConfig = builder.Configuration.GetSection("Minio").Get<MinioConfig>();

// Register infrastructure
builder.Services.AddSingleton(new MongoDbContext(mongoConfig!.ConnectionString, mongoConfig.Database));
builder.Services.AddSingleton<ICacheService>(_ => new RedisCacheService(redisConfig!.ConnectionString));
builder.Services.AddSingleton<IFileStorageService>(_ => new MinioStorageService(minioConfig!.Endpoint, minioConfig.AccessKey, minioConfig.SecretKey));

// Register domain repositories
builder.Services.AddScoped<IItemListingRepository, ItemListingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

// Register services
builder.Services.AddScoped<IItemListingService, ItemListingService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();

// Register CQRS handlers
builder.Services.AddScoped<GetAllItemListingHandler>();
builder.Services.AddScoped<GetItemListingByIdHandler>();

builder.Services.AddScoped<GetOrderByIdHandler>();
builder.Services.AddScoped<GetAllOrdersHandler>();

builder.Services.AddScoped<GetReviewsBySellerIdHandler>();
builder.Services.AddScoped<GetAllReviewsHandler>();

builder.Services.AddScoped<GetAllUserProfilesHandler>();
builder.Services.AddScoped<CreateUserProfileHandler>();

builder.Services.AddScoped<CreateOrderHandler>();
builder.Services.AddScoped<CreateItemListingHandler>();
builder.Services.AddScoped<CreateReviewHandler>();

// Build the application
var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();
app.Run();