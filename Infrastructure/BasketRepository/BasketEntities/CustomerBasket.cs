using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BasketRepository.BasketEntities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItems> BasketItems { get; set; } = new List<BasketItems>();
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
