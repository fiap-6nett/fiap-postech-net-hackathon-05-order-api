namespace FastTech.Pedidos.Application.Interfaces;

public interface IRabbitMqProducer
{
    void SendMessageToQueue(object mensagem);
}