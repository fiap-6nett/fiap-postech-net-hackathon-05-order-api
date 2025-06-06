using System.ComponentModel.DataAnnotations;

namespace FastTech.Pedidos.Domain.Enums;

public enum OrderStatus
{
    [Display(Name = "Criado")]
    Pendente = 0,

    [Display(Name = "Aceito")]
    Processando = 1,

    [Display(Name = "Rejeitado")]
    Enviado = 2,

    [Display(Name = "Cancelado")]
    Entregue = 3,

    [Display(Name = "Finalizado")]
    Devolvido = 4,

    [Display(Name = "Falhou")]
    Falhou = 5
}