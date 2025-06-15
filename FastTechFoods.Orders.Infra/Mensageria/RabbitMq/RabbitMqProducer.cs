using System.Text;
using System.Text.Json;
using FastTechFoods.Orders.Application.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FastTechFoods.Orders.Infra.Mensageria.RabbitMq;

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly RabbitMQSettings _settings;

    public RabbitMqProducer(IOptions<RabbitMQSettings> options)
    {
        _settings = options.Value;
    }
    public Task SendMessageToQueue(object mensagem)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            UserName = _settings.Username,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        
        channel.QueueDeclare(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var json = JsonSerializer.Serialize(mensagem);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(
            exchange: "",
            routingKey: _settings.QueueName,
            basicProperties: null,
            body: body
        );
        
        return Task.CompletedTask;
    }

    public Task SendMessageChangeStatusQueue(object mensagem)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            UserName = _settings.Username,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();


        channel.QueueDeclare(
            queue: _settings.QueueNameChangeStatus,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var json = JsonSerializer.Serialize(mensagem);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(
            exchange: "",
            routingKey: _settings.QueueNameChangeStatus,
            basicProperties: null,
            body: body
        );

        return Task.CompletedTask;
    }
}