namespace FastTech.Pedidos.Application.Interfaces;

public interface IRabbitMqProducer
{
    Task SendMessageToQueue(object mensagem);
}