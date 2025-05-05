using API.Config;
using Infrastructure.Cache;
using Infrastructure.Persistence;
using Infrastructure.Storage;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SecondHand E-Commerce API", Version = "v1" });
});

// Register MongoDbContext
var mongoConfig = builder.Configuration.GetSection("Mongo").Get<MongoConfig>();
builder.Services.AddSingleton(new MongoDbContext(mongoConfig!.ConnectionString, mongoConfig.Database));

// Register RedisCacheService
var redisConfig = builder.Configuration.GetSection("Redis").Get<RedisConfig>();
builder.Services.AddSingleton(new RedisCacheService(redisConfig!.ConnectionString));

// Register MinioStorageService
var minioConfig = builder.Configuration.GetSection("Minio").Get<MinioConfig>();
builder.Services.AddSingleton(new MinioStorageService(minioConfig!.Endpoint, minioConfig.AccessKey, minioConfig.SecretKey));

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();