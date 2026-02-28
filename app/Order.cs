using System;
using System.Collections.Generic;

public class Order(string customerName, List<OrderItem> items)
{
    public string CustomerName { get; set; } = customerName;
    public List<OrderItem> Items { get; set; } = items ?? [];

    public double ComputeOrderTotal()
    {
        double orderTotal = Items.Sum(item => item.GetTotalPrice());

        if(orderTotal > 500)
        {
            orderTotal *= (double)0.9;
        }

        return orderTotal;
    } 
}