using System;
using System.Linq;

namespace AdventOfCode2023.Day5
{
  internal class Program
  {
    static void Main(string[] args)
    {
      // Title
      Console.WriteLine("Code of Advent - Türchen 5\n");

      MappingHelper mappingHelper = new MappingHelper(@"Input\day-05.txt");

      // Verarbeitung
      long minLocation = mappingHelper.FromSeedsToLocations().Min();
      long minLocationWithRange = mappingHelper.FromSeedRangeToMinLocation();

      // Ausgabe
      Console.WriteLine($"Min Location: {minLocation}");
      Console.WriteLine($"Min Location with Range: {minLocationWithRange}");
    }
  }
}
