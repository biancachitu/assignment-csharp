using System;

public class OrderItem
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public OrderItem(string Name, double Price, int Quantity)
    {
        this.Name = Name;
        this.Price = Price;
        this.Quantity = Quantity;
    }

    public double GetTotalPrice()
    {
        return Quantity * Price;
    }
}