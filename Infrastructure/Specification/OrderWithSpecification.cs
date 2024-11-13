using Core.Entities;
using Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class OrderWithSpecification : Specification<Order>
    {
        public OrderWithSpecification(string buyerEmail) 
                : base(order => order.BuyerEmail == buyerEmail)
        {
            AddInClude(order => order.DeliveryMethod);
            AddInClude(order => order.OrderItems);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderWithSpecification(int id , string buyerEmail)
                : base(order => order.BuyerEmail == buyerEmail && order.Id == id)
        {
            AddInClude(order => order.DeliveryMethod);
            AddInClude(order => order.OrderItems);
            
        }
    }
}
