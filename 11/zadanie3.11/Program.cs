using System;

public interface ICommand
{
    void Execute();
}

public class GameCharacter
{
    public void Jump()
    {
        Console.WriteLine("Персонаж прыгнул.");
    }

    public void Attack()
    {
        Console.WriteLine("Персонаж атакует.");
    }

    public void Defend()
    {
        Console.WriteLine("Персонаж защищается.");
    }
}

public class JumpCommand : ICommand
{
    private GameCharacter _character;

    public JumpCommand(GameCharacter character)
    {
        _character = character;
    }

    public void Execute()
    {
        _character.Jump();
    }
}

public class AttackCommand : ICommand
{
    private GameCharacter _character;

    public AttackCommand(GameCharacter character)
    {
        _character = character;
    }

    public void Execute()
    {
        _character.Attack();
    }
}

public class DefendCommand : ICommand
{
    private GameCharacter _character;

    public DefendCommand(GameCharacter character)
    {
        _character = character;
    }

    public void Execute()
    {
        _character.Defend();
    }
}

public class GameController
{
    private ICommand _jumpCommand;
    private ICommand _attackCommand;
    private ICommand _defendCommand;

    public GameController(ICommand jumpCommand, ICommand attackCommand, ICommand defendCommand)
    {
        _jumpCommand = jumpCommand;
        _attackCommand = attackCommand;
        _defendCommand = defendCommand;
    }

    public void Jump()
    {
        _jumpCommand.Execute();
    }

    public void Attack()
    {
        _attackCommand.Execute();
    }

    public void Defend()
    {
        _defendCommand.Execute();
    }
}

class Program
{
    static void Main(string[] args)
    {
        GameCharacter character = new GameCharacter();

        ICommand jumpCommand = new JumpCommand(character);
        ICommand attackCommand = new AttackCommand(character);
        ICommand defendCommand = new DefendCommand(character);

        GameController controller = new GameController(jumpCommand, attackCommand, defendCommand);

        controller.Jump();   
        controller.Attack(); 
        controller.Defend(); 
    }
}