﻿using iParty.Business.Models.Items;

namespace iParty.Business.Models.Orders
{
    public class OrderItem: Entity
    {
        public OrderItem() : base() { }
        public OrderItem(ItemForOrder item, MeasurementUnit unit, int quantity, decimal price)
        {
            Item = item;
            Unit = unit;
            Quantity = quantity;
            Price = price;            
        }
        public ItemForOrder Item  { get; private set; }        
        public MeasurementUnit Unit { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Total { get; private set; }
        public decimal CalculateTotal()
        {
            return Quantity * Price;
        }
        public void TotalizeOrderItem()
        {
            Total = this.CalculateTotal();
        }
        public void CopyData(OrderItem source)
        {
            if (source == null) return;
            
            Item = source.Item;
            Quantity = source.Quantity;
        }
    }
}
