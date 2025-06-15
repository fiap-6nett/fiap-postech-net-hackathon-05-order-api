namespace FastTechFoods.Orders.Application.Interfaces;

public interface IRabbitMqProducer
{
    Task SendMessageToQueue(object mensagem);
    Task SendMessageChangeStatusQueue(object mensagem);
}