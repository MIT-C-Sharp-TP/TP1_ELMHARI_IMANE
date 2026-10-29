using System;
using System.Collections.Generic;

Random rng = new Random(7);
List<int> winningTurns = new List<int>();
int gameNumber = 0;

Console.WriteLine("=== ARENA: AYA VS THE ENEMIES ===");

while (true)
{
    gameNumber++;
    int turns = PlayGame(rng);
    string result = turns > 0 ? $"Game {gameNumber}: victory in {turns} turns" : $"Game {gameNumber}: defeat";
    SaveResult(result);

    if (turns > 0)
    {
        winningTurns.Add(turns);
    }

    Console.Write("Play again? (y/n) > ");
    string? answer = Console.ReadLine();
    if (!string.Equals(answer?.Trim(), "y", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }
}

Console.WriteLine();
Console.WriteLine($"Victories: {winningTurns.Count} of {gameNumber}");
Console.WriteLine("Best victories (fewest turns first):");
int rank = 0;
foreach (int turns in winningTurns.OrderBy(t => t).Take(3))
{
    rank++;
    Console.WriteLine($"- {turns} turns");
}
if (rank == 0)
{
    Console.WriteLine("- No victories yet.");
}

int PlayGame(Random rng)
{
    Player player = new Player("Aya", 100, 2);
    player.Died += name => Console.WriteLine($"Game over for {name}.");

    List<Enemy> enemies = new List<Enemy>
    {
        new Goblin(),
        new Orc(),
        new Goblin(),
        new Boss()
    };

    Console.WriteLine();
    Console.WriteLine($"{player.Name} faces {enemies.Count} enemies:");
    foreach (Enemy enemy in enemies)
    {
        Console.WriteLine($"- {enemy.Name}, {enemy.Health} HP");
    }

    int turn = 0;
    while (player.IsAlive && enemies.Count > 0)
    {
        turn++;
        Enemy enemy = enemies[0];

        Console.WriteLine();
        Console.WriteLine($"Turn {turn}: {player.Name} {player.Health} HP, {player.Potions} potions | {enemy.Name} {enemy.Health} HP");
        Console.Write("1 attack, 2 drink a potion > ");
        string? text = Console.ReadLine();

        if (!int.TryParse(text, out int choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Type 1 or 2.");
            continue;
        }

        if (choice == 1)
        {
            enemy.TakeDamage(rng.Next(12, 25));
            if (!enemy.IsAlive)
            {
                Console.WriteLine($"{enemy.Name} is defeated!");
                enemies.RemoveAt(0);
                if (enemies.Count == 0)
                {
                    Console.WriteLine($"Victory in {turn} turns!");
                    return turn;
                }
                continue;
            }
        }
        else
        {
            player.DrinkPotion();
            if (!player.IsAlive)
            {
                break;
            }
        }

        if (enemy.IsAlive && player.IsAlive)
        {
            enemy.Attack(player, rng);
        }
    }

    Console.WriteLine("Defeat...");
    return 0;
}

void SaveResult(string line)
{
    try
    {
        File.AppendAllText("results.txt", line + Environment.NewLine);
        Console.WriteLine($"Saved: {line}");
    }
    catch (IOException e)
    {
        Console.WriteLine($"Could not save the result: {e.Message}");
    }
    catch (UnauthorizedAccessException e)
    {
        Console.WriteLine($"Could not save the result: {e.Message}");
    }
}

public class Player
{
    private int health;

    public string Name { get; }
    public int MaxHealth { get; }
    public int Potions { get; private set; }
    public int Health
    {
        get => health;
        private set => health = Math.Clamp(value, 0, MaxHealth);
    }
    public bool IsAlive => Health > 0;
    public event Action<string>? Died;

    public Player(string name, int maxHealth, int potions)
    {
        Name = name;
        MaxHealth = maxHealth;
        Potions = potions;
        Health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        bool wasAlive = IsAlive;
        Health -= Math.Max(0, amount);
        Console.WriteLine($"{Name} takes {amount} damage ({Health} HP)");
        if (wasAlive && !IsAlive)
        {
            Died?.Invoke(Name);
        }
    }

    public void DrinkPotion()
    {
        if (Potions <= 0)
        {
            Console.WriteLine("No potion left!");
            return;
        }
        if (Health == MaxHealth)
        {
            Console.WriteLine("Health is already full; potion was not used.");
            return;
        }
        Potions--;
        Health += 30;
        Console.WriteLine($"{Name} drinks a potion ({Health} HP)");
    }
}

public abstract class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    public bool IsAlive => Health > 0;

    protected Enemy(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public virtual void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - Math.Max(0, amount));
        Console.WriteLine($"{Name} takes {amount} damage ({Health} HP)");
    }

    public abstract void Attack(Player target, Random rng);
}

public class Goblin : Enemy
{
    public Goblin() : base("Goblin", 30) { }

    public override void Attack(Player target, Random rng)
    {
        Console.WriteLine("Goblin stabs!");
        target.TakeDamage(rng.Next(4, 9));
    }
}

public class Orc : Enemy
{
    public Orc() : base("Orc", 60) { }

    public override void Attack(Player target, Random rng)
    {
        Console.WriteLine("Orc swings its club!");
        target.TakeDamage(rng.Next(8, 15));
    }
}

public class Boss : Enemy
{
    public int Armour { get; } = 6;

    public Boss() : base("Boss", 60) { }

    public override void TakeDamage(int amount)
    {
        int reduced = Math.Max(amount - Armour, 1);
        Console.WriteLine($"The armour absorbs {amount - reduced} damage.");
        base.TakeDamage(reduced);
    }

    public override void Attack(Player target, Random rng)
    {
        Console.WriteLine("Boss slams the ground!");
        target.TakeDamage(rng.Next(9, 17));
    }
}
