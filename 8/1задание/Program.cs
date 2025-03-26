using System;
using System.Collections;

namespace BankQueueExample
{
    public class Customer
    {
        public int Id { get; }
        public string Name { get; }
        public string ServiceType { get; }

        public Customer(int id, string name, string serviceType)
        {
            Id = id;
            Name = name;
            ServiceType = serviceType;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, ServiceType: {ServiceType}";
        }
    }

    public class BankQueue
    {
        private Queue _customerQueue;

        public BankQueue()
        {
            _customerQueue = new Queue();
        }

        public void EnqueueCustomer(Customer customer)
        {
            _customerQueue.Enqueue(customer);
            Console.WriteLine($"{customer.Name} добавлен в очередь.");
        }

        public Customer DequeueCustomer()
        {
            if (_customerQueue.Count == 0)
            {
                Console.WriteLine("Очередь пуста.");
                return null;
            }

            Customer customer = (Customer)_customerQueue.Dequeue();
            Console.WriteLine($"{customer.Name} обслужен и удален из очереди.");
            return customer;
        }

        public Customer FindCustomerById(int id)
        {
            foreach (Customer customer in _customerQueue)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            Console.WriteLine($"Клиент с ID {id} не найден.");
            return null;
        }

        public void ShowAllCustomers()
        {
            Console.WriteLine("Клиенты в очереди:");
            foreach (Customer customer in _customerQueue)
            {
                Console.WriteLine(customer);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BankQueue bankQueue = new BankQueue();

            bankQueue.EnqueueCustomer(new Customer(1, "Kate", "Deposit"));
            bankQueue.EnqueueCustomer(new Customer(2, "Ann", "Withdrawal"));
            bankQueue.EnqueueCustomer(new Customer(3, "Sam", "Credit"));

            bankQueue.ShowAllCustomers();

            bankQueue.FindCustomerById(2);

            bankQueue.DequeueCustomer();  
            bankQueue.DequeueCustomer(); 

            bankQueue.ShowAllCustomers();
        }
    }
}