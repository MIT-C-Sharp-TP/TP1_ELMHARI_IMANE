using System;

int Heal(int currentHealth, int amount, int maxHealth)
{
    return Math.Min(currentHealth + amount, maxHealth);
}

Console.WriteLine(Heal(40, 20, 100));   // 60
Console.WriteLine(Heal(90, 20, 100));   // 100
Console.WriteLine(Heal(100, 20, 100));  // 100
