using System;

public abstract class ElectronicDevice
{
    public abstract void TurnOn();

    public virtual void TurnOff()
    {
        Console.WriteLine("Device is turning off");
    }
}

public class TV : ElectronicDevice
{
    public override void TurnOn()
    {
        Console.WriteLine("TV is turning on");
    }

    public override void TurnOff()
    {
        Console.WriteLine("TV is turning off");
    }
}

public class Radio : ElectronicDevice
{
    public override void TurnOn()
    {
        Console.WriteLine("Radio is turning on");
    }

    public override void TurnOff()
    {
        Console.WriteLine("Radio is turning off");
    }
}

public class Program
{
    public static void Main()
    {
        ElectronicDevice myTV = new TV();
        myTV.TurnOn();    
        myTV.TurnOff();  

        Console.WriteLine(); 

        ElectronicDevice myRadio = new Radio();
        myRadio.TurnOn();   
        myRadio.TurnOff(); 
    }
}