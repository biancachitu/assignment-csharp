using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class Program
{
    static List<Order> InitializeOrderList()
    {
        OrderItem laptop = new("Laptop", 2500.0, 2);
        OrderItem universalRemote = new("Universal Remote", 30.0, 2578);
        OrderItem mechanicalKeyboard = new("Mechanical Keyboard Light - keys not included", 280.0, 13);
        OrderItem PS2case = new("PlayStation 2 Empty Case", 2.0, 68);
        OrderItem bionicEye = new("Bionic Eye slightly used", 1200.0, 1);
        OrderItem mcSecurityCamera = new("Minecraft Security Camera", 4.92, 4);
        OrderItem kitchenScale = new("Chicken Scale", 18.99, 1222);

        // Joe Dane and Joe Lane are both considered top customers
        return [
            new Order("Jane Doe", [laptop, mechanicalKeyboard]),
            new Order("John Doe", [universalRemote, bionicEye, PS2case]),
            new Order("Joe Dane", [PS2case, universalRemote]),
            new Order("Joe Dane", [bionicEye, mcSecurityCamera, kitchenScale]),
            new Order("Joe Lane", [PS2case, universalRemote]),
            new Order("Joe Lane", [bionicEye, mcSecurityCamera, kitchenScale])
        ];
    }

    static void Main()
    {
        var orders = InitializeOrderList();

        if (orders.Count == 0)
        {
            Console.WriteLine("There is no top customer in the database.");
        }

        var topCustomers =  GetTopCustomer(orders);
        
        var displayString = topCustomers.Count == 1 
            ? $"Top customer is: {topCustomers.FirstOrDefault()}! Yay!\nTop 3 most popular products up next!"
            : $"Top customers are: {string.Join(", ", topCustomers)}! Yay!\nTop 3 most popular products up next!";

        Console.WriteLine(displayString);

        var popularItems = GetPopularProducts(orders);

        foreach (var (ProductName, TotalSold) in popularItems)
        {
            Console.WriteLine($"{ProductName}: {TotalSold} sold.");
        }
    }


    public static List<string> GetTopCustomer(List<Order> orders)
    {
        if (orders == null || orders.Count == 0)
        {
            return [];
        }

        var customerTotals = orders
            .GroupBy(o => o.CustomerName)
            .Select(g => new
            {
                CustomerName = g.Key,
                TotalSpent = g.Sum(o => o.ComputeOrderTotal())
            })
            .ToList();

        var maxSpent = customerTotals.Max(c => c.TotalSpent);

        var topCustomers = customerTotals
            .Where(c => c.TotalSpent == maxSpent)
            .Select(c => c.CustomerName)
            .ToList();

        return topCustomers;
    }

    public static List<(string ProductName, int TotalSold)> GetPopularProducts(List<Order> orders)
    {
        return orders
            .SelectMany(order => order.Items)
            .GroupBy(item => item.Name)
            .Select(group => (
                ProductName: group.Key,
                TotalSold: group.Sum(IteratorStateMachineAttribute => IteratorStateMachineAttribute.Quantity)
            ))
            .OrderByDescending(x => x.TotalSold)
            .Take(3)
            .ToList();
    }
}
