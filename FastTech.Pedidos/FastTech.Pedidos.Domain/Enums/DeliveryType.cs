using System.ComponentModel.DataAnnotations;

namespace FastTech.Pedidos.Domain.Enums;

public enum DeliveryType
{
    [Display(Name = "Balção")]
    Pendente = 0,

    [Display(Name = "Delilvery")]
    Processando = 1,

    [Display(Name = "Drive-thru")]
    Enviado = 2,
}