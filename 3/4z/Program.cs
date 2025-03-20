using System;
using System.Collections.Generic;
using System.Linq;

public partial class User
{
    public string Username { get; set; }
    public int FollowersCount { get; set; }
    public int PostsCount { get; set; }
    public DateTime LastActiveDate { get; set; }

    public User(string username, int followersCount, int postsCount, DateTime lastActiveDate)
    {
        Username = username;
        FollowersCount = followersCount;
        PostsCount = postsCount;
        LastActiveDate = lastActiveDate;
    }
}

public partial class User
{
    public bool IsActive(int days)
    {
        return (DateTime.Now - LastActiveDate).TotalDays <= days;
    }
}

public class SocialNetwork
{
    private User[] users;

    public SocialNetwork(User[] users)
    {
        this.users = users;
    }

    public List<User> GetMostPopularUsers(int minFollowers)
    {
        return users.Where(user => user.FollowersCount > minFollowers).ToList();
    }

    public List<User> GetInactiveUsers(int days)
    {
        return users.Where(user => !user.IsActive(days)).ToList();
    }
}

public class Program
{
    public static void Main()
    {
        User[] users = new User[]
        {
            new User("Alice", 150, 10, DateTime.Now.AddDays(-1)),
            new User("Sam", 200, 15, DateTime.Now.AddDays(-5)),
            new User("Kate", 50, 20, DateTime.Now.AddDays(-10)),
            new User("Din", 300, 5, DateTime.Now.AddDays(-30)),
            new User("Kim", 10, 0, DateTime.Now.AddDays(-15))
        };

        SocialNetwork socialNetwork = new SocialNetwork(users);

        Console.Write("Введите минимальное количество подписчиков для поиска популярных пользователей: ");
        int minFollowers = int.Parse(Console.ReadLine());
        var popularUsers = socialNetwork.GetMostPopularUsers(minFollowers);

        Console.WriteLine($"Популярные пользователи с более чем {minFollowers} подписчиками:");
        foreach (var user in popularUsers)
        {
            Console.WriteLine($"- {user.Username} ({user.FollowersCount} подписчиков)");
        }

        Console.Write("Введите количество дней для определения неактивных пользователей: ");
        int inactiveDays = int.Parse(Console.ReadLine());
        var inactiveUsers = socialNetwork.GetInactiveUsers(inactiveDays);

        Console.WriteLine($"Неактивные пользователи (не заходили более {inactiveDays} дней):");
        foreach (var user in inactiveUsers)
        {
            Console.WriteLine($"- {user.Username} (Последняя активность: {user.LastActiveDate.ToShortDateString()})");
        }
    }
}