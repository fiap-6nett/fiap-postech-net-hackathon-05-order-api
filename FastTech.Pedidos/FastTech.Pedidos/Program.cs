using FastTech.Pedidos.Application.Interfaces;
using FastTech.Pedidos.Application.Services;
using FastTech.Pedidos.Infra.Mensageria.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IOrderService, OderService>();
builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();