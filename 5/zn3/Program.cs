using System;
using System.Collections.Generic;

public abstract class OfficeEquipment
{
    public string Model { get; set; }
    public abstract void ShowInfo();
}

public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public class LaserPrinter : OfficeEquipment, IPrinter
{
    public override void ShowInfo()
    {
        Console.WriteLine($"Laser Printer Model: {Model}");
    }

    public void Print()
    {
        Console.WriteLine($"{Model} is printing a document...");
    }
}

public class DocumentScanner : OfficeEquipment, IScanner
{
    public override void ShowInfo()
    {
        Console.WriteLine($"Document Scanner Model: {Model}");
    }

    public void Scan()
    {
        Console.WriteLine($"{Model} is scanning a document...");
    }
}

public class Program
{
    public static void Main()
    {
        OfficeEquipment[] officeEquipments = new OfficeEquipment[]
        {
            new LaserPrinter { Model = "HP LaserJet Pro" },
            new DocumentScanner { Model = "Canon ImageFORMULA" },
            new LaserPrinter { Model = "Brother HL-L2350DW" },
            new DocumentScanner { Model = "Epson WorkForce" }
        };

        List<DocumentScanner> scanners = new List<DocumentScanner>();

        foreach (var equipment in officeEquipments)
        {
            if (equipment is DocumentScanner scanner) 
            {
                scanners.Add(scanner);
            }
        }

        Console.WriteLine("Found Scanner Models:");
        foreach (var scanner in scanners)
        {
            scanner.ShowInfo();
        }

        foreach (var scanner in scanners)
        {
            scanner.Scan(); 
        }
    }
}