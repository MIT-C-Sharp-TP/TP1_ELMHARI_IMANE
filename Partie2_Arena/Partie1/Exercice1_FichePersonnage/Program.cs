using System;
using System.Globalization;

string name = "Aya";
int level = 3;
int health = 100;
float speed = 5.5f;
bool hasKey = false;

Console.WriteLine($"Name:   {name}");
Console.WriteLine($"Level:  {level}");
Console.WriteLine($"Health: {health} HP");
Console.WriteLine($"Speed:  {speed.ToString("0.0", CultureInfo.InvariantCulture)} m/s");
Console.WriteLine($"Has key: {hasKey}");
Console.WriteLine($"XP for next level: {level * 100}");
