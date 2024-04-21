using System.Runtime.Serialization;

namespace Projekt_Inżynierski.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Utworzono")]
        Created,

        [EnumMember(Value ="Potwierdzono")]
        OrderConfirmed,
    }
}