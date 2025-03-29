using System;

public interface ITicket
{
    void Book();
}

public class PlaneTicket : ITicket
{
    public void Book()
    {
        Console.WriteLine("Билет на самолет забронирован.");
    }
}

public class TrainTicket : ITicket
{
    public void Book()
    {
        Console.WriteLine("Билет на поезд забронирован.");
    }
}

public class BusTicket : ITicket
{
    public void Book()
    {
        Console.WriteLine("Билет на автобус забронирован.");
    }
}

public abstract class TicketFactory
{
    public abstract ITicket CreateTicket();
}

public class PlaneFactory : TicketFactory
{
    public override ITicket CreateTicket()
    {
        return new PlaneTicket();
    }
}

public class TrainFactory : TicketFactory
{
    public override ITicket CreateTicket()
    {
        return new TrainTicket();
    }
}

public class BusFactory : TicketFactory
{
    public override ITicket CreateTicket()
    {
        return new BusTicket();
    }
}

class Program
{
    static void Main(string[] args)
    {
        TicketFactory planeFactory = new PlaneFactory();
        TicketFactory trainFactory = new TrainFactory();
        TicketFactory busFactory = new BusFactory();

        ITicket planeTicket = planeFactory.CreateTicket();
        planeTicket.Book(); 

        ITicket trainTicket = trainFactory.CreateTicket();
        trainTicket.Book(); 

        ITicket busTicket = busFactory.CreateTicket();
        busTicket.Book(); 
    }
}