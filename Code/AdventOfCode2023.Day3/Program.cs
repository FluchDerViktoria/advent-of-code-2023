using System;
using System.Linq;

namespace AdventOfCode2023.Day3
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // Title
      Console.WriteLine("Code of Advent - Türchen 3\n");

      // Get EngineData
      EngineSchematic engineSchematic = new EngineSchematic(@"Input\day-03.txt");

      // Calculating
      int sumOfPartNumbers = engineSchematic.GetAllPartNumbers().Sum();
      int sumOfGearRatios = engineSchematic.GetAllGearRatios().Sum();

      // Ausgabe
      Console.WriteLine($"Summe aller Teilnummern: {sumOfPartNumbers}");
      Console.WriteLine($"Summe aller Gear Verhältnisse: {sumOfGearRatios}");
    }
  }
}
