using System;

public delegate void OrderHandler(string order);

public class CookOrder
{
    public void PreparedOrder(string order)
    {
        Console.WriteLine($"Cooking order: {order}");
    }
}

public class DeliverOrder
{
    public void DeliverOrderToCustomer(string order)
    {
        Console.WriteLine($"Delivery order: {order}");
    }
}

public class Program
{
    public static void Main()
    {
        CookOrder cook = new CookOrder();
        DeliverOrder deliver = new DeliverOrder();

        OrderHandler orderHandler;

        orderHandler = cook.PreparedOrder;
        orderHandler("Sushi");

        orderHandler = deliver.DeliverOrderToCustomer;
        orderHandler("Sushi");
    }
}