using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderEntities
{
    public enum OrderStatus
    {
        Pending,
        PaymentRecieved,
        PaymentFaild
    }
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
