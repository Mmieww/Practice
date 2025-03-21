using System;

public class Program
{
    public static void Main()
    {
        double A1 = 1.0, B1 = 2.0, C1 = 3.0;
        Console.WriteLine($"До сдвига: A1={A1}, B1={B1}, C1={C1}");
        ShiftLeft3(ref A1, ref B1, ref C1);
        Console.WriteLine($"После сдвига: A1={A1}, B1={B1}, C1={C1}");

        double A2 = 4.0, B2 = 5.0, C2 = 6.0;
        Console.WriteLine($"\nДо сдвига: A2={A2}, B2={B2}, C2={C2}");
        ShiftLeft3(ref A2, ref B2, ref C2);
        Console.WriteLine($"После сдвига: A2={A2}, B2={B2}, C2={C2}");
    }

    public static void ShiftLeft3(ref double A, ref double B, ref double C)
    {
        double temp = A; 
        A = B;           
        B = C;           
        C = temp;      
    }
}