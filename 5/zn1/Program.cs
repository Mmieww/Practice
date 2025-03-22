using System;

public abstract class DeliveryMethod
{
    public abstract void Deliver();
}

public class Courier : DeliveryMethod
{
    public override void Deliver()
    {
        Console.WriteLine("Delivery by Courier: Your package will arrive at your door.");
    }
}

public class Pickup : DeliveryMethod
{
    public override void Deliver()
    {
        Console.WriteLine("Pickup: Your package is ready for pickup at the nearest store.");
    }
}

public class Post : DeliveryMethod
{
    public override void Deliver()
    {
        Console.WriteLine("Delivery by Post: Your package will be sent to the provided address via postal service.");
    }
}

public class Program
{
    public static void Main()
    {
        DeliveryMethod[] deliveryMethods = new DeliveryMethod[]
        {
            new Courier(),
            new Pickup(),
            new Post()
        };

        foreach (var method in deliveryMethods)
        {
            method.Deliver();
        }
    }
}