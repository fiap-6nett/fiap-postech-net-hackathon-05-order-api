using FastTechFoods.Orders.Application.Interfaces;
using FastTechFoods.Orders.Application.Services;
using FastTechFoods.Orders.Domain.Interfaces;
using FastTechFoods.Orders.Infra.Mensageria.RabbitMq;
using FastTechFoods.Orders.Infra.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoDbSettings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(mongoDbSettings.ConnectionString);
});

// Repositórios e AutoMapper
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

// Middleware Prometheus para requisições HTTP
app.UseHttpMetrics();

app.UseAuthorization();

app.MapControllers();

app.MapMetrics(); // Exposição do /metrics

app.Run();