using System;
using System.Collections.Generic;

public class ForexMarket
{
    private List<ICurrencyObserver> _observers = new List<ICurrencyObserver>();
    private Dictionary<string, decimal> _currencyRates = new Dictionary<string, decimal>();

 
    public void Subscribe(ICurrencyObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(ICurrencyObserver observer)
    {
        _observers.Remove(observer);
    }

    public void UpdateCurrencyRate(string currency, decimal rate)
    {
        _currencyRates[currency] = rate;
        NotifyObservers(currency, rate);
    }

    private void NotifyObservers(string currency, decimal rate)
    {
        foreach (var observer in _observers)
        {
            observer.Update(currency, rate);
        }
    }
}

public interface ICurrencyObserver
{
    void Update(string currency, decimal rate);
}

public class Trader : ICurrencyObserver
{
    private string _name;

    public Trader(string name)
    {
        _name = name;
    }

    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Трейдер {_name} получил обновление: {currency} = {rate}");
    }
}

public class Bank : ICurrencyObserver
{
    private string _bankName;

    public Bank(string bankName)
    {
        _bankName = bankName;
    }

    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Банк {_bankName} получил обновление: {currency} = {rate}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ForexMarket forexMarket = new ForexMarket();
        Trader trader1 = new Trader("Екатерина");
        Trader trader2 = new Trader("Анастасия");
        Bank bank1 = new Bank("Банк А");
        Bank bank2 = new Bank("Банк Б");

        forexMarket.Subscribe(trader1);
        forexMarket.Subscribe(trader2);
        forexMarket.Subscribe(bank1);
        forexMarket.Subscribe(bank2);

        forexMarket.UpdateCurrencyRate("USD", 73.64m);
        forexMarket.UpdateCurrencyRate("EUR", 85.45m);
        forexMarket.UpdateCurrencyRate("BLR", 100.25m);

        forexMarket.Unsubscribe(trader2);

        forexMarket.UpdateCurrencyRate("ZLT", 0.67m);
    }
}