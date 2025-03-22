using System;

public class Developer
{
    public string Name { get; private set; }

    public Developer(string name)
    {
        Name = name;
    }

    public void DevelopGame(VideoGame game)
    {
        Console.WriteLine($"{Name} is developing the game: {game.Title}");
    }
}

public class Player
{
    public string PlayerName { get; private set; }

    public Player(string playerName)
    {
        PlayerName = playerName;
    }
}

public class GameWorld
{
    public string WorldName { get; private set; }

    public GameWorld(string worldName)
    {
        WorldName = worldName;
    }

    public void CreateWorld()
    {
        Console.WriteLine($"Game world '{WorldName}' has been created.");
    }
}

public class VideoGame
{
    public string Title { get; private set; }
    private Player[] players;
    private GameWorld gameWorld; 
    private Developer developer;

    public VideoGame(string title, Developer developer, string worldName)
    {
        Title = title;
        this.developer = developer;
        gameWorld = new GameWorld(worldName); 
        developer.DevelopGame(this);
    }

    public void AddPlayers(params Player[] newPlayers)
    {
        players = newPlayers;
    }

    public void StartGame()
    {
        gameWorld.CreateWorld(); 
        Console.WriteLine($"Starting game: {Title}");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.PlayerName} has joined the game.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Developer developer = new Developer("Epic Games");

        VideoGame[] videoGames = new VideoGame[]
        {
            new VideoGame("Fortnite", developer, "Island"),
            new VideoGame("Gears of War", developer, "Sera"),
            new VideoGame("Unreal Tournament", developer, "Arena")
        };

        Player player1 = new Player("Kate");
        Player player2 = new Player("Pavel");

        videoGames[0].AddPlayers(player1, player2);
        videoGames[1].AddPlayers(new Player("Sam"));
        videoGames[2].AddPlayers(new Player("Dima"));

        foreach (var game in videoGames)
        {
            game.StartGame();
            Console.WriteLine(); 
        }
    }
}