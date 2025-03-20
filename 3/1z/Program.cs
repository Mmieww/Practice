using System;

class A
{
    private int a;
    private int b;

    public A(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public double CalculateExpression()
    {
        return Math.Pow(4.0 / (a + 2), b);
    }

    public double CalculateB()
    {
        return Math.Pow(b, 10);
    }

    public void DisplayValues()
    {
        Console.WriteLine($"a: {a}");
        Console.WriteLine($"b: {b}");
        Console.WriteLine($"(4/(a+2))^b: {CalculateExpression():F2}");
        Console.WriteLine($"b^10: {CalculateB()}");
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Введите значение а: ");
            int a = int.Parse( Console.ReadLine() );

            Console.Write("Введите значение b: ");
            int b = int.Parse(Console.ReadLine());

            A obj = new A(a, b);
            obj.DisplayValues();
        }
    }
}