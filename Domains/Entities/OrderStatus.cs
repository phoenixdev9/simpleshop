using System.Runtime.Serialization;

namespace Domains.Entities {
    public enum OrderStatus {
        [EnumMember (Value = "Order Pending")]
        Pending,

        [EnumMember (Value = "Order Shipped")]
        Shipped,

        [EnumMember (Value = "Order Delivered")]
        Delivered
    }
}