﻿using iParty.Business.Models.Addresses;
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
        public decimal ItemsTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Installments { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PartyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<OrderItem> Items { get; set; }        

        public void SetDefaultValuesForNewOrder(List<OrderItemPrice> prices)
        {
            DateTime = DateTime.Now;

            ExpirationDate = DateTime.AddDays(7);

            Status = OrderStatus.Draft;

            setItemsPrices(prices);
        }

        public void SetDefaultValuesForUpdatedOrder(Order currentOrder, List<OrderItemPrice> prices)
        {
            DateTime = currentOrder.DateTime;

            ExpirationDate = currentOrder.ExpirationDate;

            Status = currentOrder.Status;

            setItemsPrices(prices);
        }

        public decimal CalculateItemsTotal()
        {
            return Items.Sum(x => x.Total);
        }

        public decimal CalculateOrderTotal()
        {
            return CalculateItemsTotal() + Freight;
        }

        public void TotalizeOrder()
        {
            foreach (var item in Items)
            {
                item.Total = item.CalculateTotal();
            }                

            ItemsTotal = CalculateItemsTotal();

            OrderTotal = CalculateOrderTotal();
        }

        private void setItemsPrices(List<OrderItemPrice> prices)
        {
            foreach (var price in prices)
            {
                Items.First(x => x.Id == price.ItemId).Price = price.Price;
            }
        }
    }
}
