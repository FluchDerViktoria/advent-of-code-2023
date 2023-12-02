using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Day2
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // Title
      Console.WriteLine("Code of Advent - Türchen 2\n");

      // Spiele laden
      List<Game> games = Game.LoadGamesFromFile(@"Input\day-02.txt");

      // Max Cubes
      int maxCubeRed = 12;
      int maxCubeGreen = 13;
      int maxCubeBlue = 14;

      // Summen Bestimmen
      int sumOfValidId = 0;
      int sumOfPower = 0;
      games.ForEach(g =>
      {
        // Gültige ID
        if (g.IsGameValid(maxCubeRed, maxCubeGreen, maxCubeBlue))
          sumOfValidId += g.ID;

        // Power
        sumOfPower += g.GetPower();
      });

      // Ausgabe
      Console.WriteLine($"Summe IDs aller gültigen Spiele: {sumOfValidId}");
      Console.WriteLine($"Summe der Power: {sumOfPower}");

    }
  }
}
