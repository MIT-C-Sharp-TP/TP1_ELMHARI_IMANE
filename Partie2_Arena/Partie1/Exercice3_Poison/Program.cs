using System;

int health = 40;
int poisonDamage = 8;

for (int turn = 1; turn <= 6; turn++)
{
    health = Math.Max(0, health - poisonDamage);
    Console.WriteLine($"Turn {turn}: Aya has {health} HP");

    if (health == 0)
    {
        Console.WriteLine("Aya is down.");
        break;
    }
}
