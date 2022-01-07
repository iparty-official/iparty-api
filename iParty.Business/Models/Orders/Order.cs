using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Business.Models.Orders
{
    public class Order: Entity
    {
        public DateTime DateTime { get; set; }
        public PersonForOrder Supplier { get; set; }
        public PersonForOrder Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Freight { get; set; }
        public decimal PaymentPlanFee { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public PaymentPlanForOrder PaymentPlan { get; set; }        
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PartyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<OrderItem> Items { get; set; }

        public DateTime CalculateExpirationDate(DateTime baseDateTime)
        {
            return baseDateTime.AddDays(7);
        }

        public void SetDefaultValuesForNewOrder()
        {
            DateTime = DateTime.Now;

            ExpirationDate = CalculateExpirationDate(DateTime);

            Status = OrderStatus.Draft;
        }

        public void CopyHeaderData(Order source)
        {
            Supplier = source.Supplier;
            Customer = source.Customer;
            ShippingAddress = source.ShippingAddress;
            Freight = source.Freight;
            PaymentPlan = source.PaymentPlan;
            Notes = source.Notes;
            PartyDate = source.PartyDate;            
        }

        public void CopyItemsData(Order newOrder)
        {
            foreach (var item in Items)
            {
                item.CopyData(newOrder.Items.Find(x => x.Item.Id == item.Item.Id));
            }
        }

        public decimal CalculateItemsTotal()
        {
            return Items.Sum(x => x.Total);
        }

        public decimal CalculatePaymentPlanFee()
        {
            return Math.Round((this.ItemsTotal + this.Freight) * PaymentPlan.Fee / 100, 2);
        }        

        public decimal CalculateOrderTotal()
        {
            return CalculateItemsTotal() + Freight + PaymentPlanFee;
        }

        public void TotalizeOrder()
        {
            foreach (var item in Items)
            {
                item.Total = item.CalculateTotal();
            }                

            ItemsTotal = CalculateItemsTotal();

            PaymentPlanFee = CalculatePaymentPlanFee();

            OrderTotal = CalculateOrderTotal();
        }

        public void ReplaceItem(Guid currentOrderItemId, OrderItem newOrderItem)
        {
            var currentOrderItem = Items.Find(x => x.Id == currentOrderItemId);

            if (currentOrderItem == null)
                throw new Exception("Não foi possível localizar o item de pedido informado.");

            var index = Items.IndexOf(currentOrderItem);

            Items.Remove(currentOrderItem);

            newOrderItem.Id = currentOrderItemId;

            Items.Insert(index, newOrderItem);            
        }

        internal void RemoveItem(Guid orderItemId)
        {
            var currentOrderItem = Items.Find(x => x.Id == orderItemId);

            if (currentOrderItem == null)
                throw new Exception("Não foi possível localizar o item de pedido informado");

            var index = Items.IndexOf(currentOrderItem);

            Items.Remove(currentOrderItem);            
        }
    }
}
