using System.Runtime.Serialization;

namespace Projekt_Inżynierski.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Created")]
        Created,

        [EnumMember(Value ="TransactionConfirmed")]
        TransactionConfirmed,

        [EnumMember(Value ="TransactionError")]
        TransactionError,

        [EnumMember(Value ="OrderConfirmed")]
        OrderConfirmed,
    }
}