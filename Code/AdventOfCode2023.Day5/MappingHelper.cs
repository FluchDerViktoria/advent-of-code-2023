using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day5
{
  public class MappingHelper
  {
    public MapType[] MapOrder { get; private set; } = { MapType.Seed, MapType.Soil, MapType.Fertilizer, MapType.Water, MapType.Light, MapType.Temperature, MapType.Humidity, MapType.Location };
    public List<long> Seeds { get; private set; }
    public List<SourceToDestinationMap> Maps { get; private set; }

    public MappingHelper(string filePath) 
    {
      Maps = new List<SourceToDestinationMap>();
      Seeds = new List<long>();

      LoadFromFile(filePath);

    }

    private void LoadFromFile(string filePath)
    {
      MapType source = MapType.Seed;
      MapType destination = MapType.Seed;

      foreach (string line in File.ReadLines(filePath))
      {
        // Prüft, ob leer
        if (string.IsNullOrWhiteSpace(line))
          continue;

        // Teste auf Kopfzeile ("seeds:")
        if (line.StartsWith("seeds:"))
        {
          Seeds = ExtractSeeds(line).ToList();
          continue;
        }

        // Testet auf Nummern (immer 3 an der Zahl)
        string[] numberStrings = line.Split(" ");
        if (numberStrings.Length == 3)
        {
          Maps.Add(new SourceToDestinationMap()
          {
            DestinationStart = long.Parse(numberStrings[0]),
            SourceStart = long.Parse(numberStrings[1]),
            Range = long.Parse(numberStrings[2]),
            DestinationType = destination,
            SourceType = source
          });

          continue;
        }

        // Sonst: Annahme, dass Neuer Map-Title ist
        MapType[] mapTypes = TitleToMapType(line);

        if(mapTypes.Length == 2)
        {
          source = mapTypes[0];
          destination = mapTypes[1];
        }

      }
    }

    private IEnumerable<long> ExtractSeeds(string headerLine)
    {
      string[] splitted = headerLine.Split(" ");

      foreach (string number in splitted)
      {
        if (number.StartsWith("seeds:")) // Dies nicht parsen
          continue;

        yield return long.Parse(number);
      }
    }

    private MapType[] TitleToMapType(string title)
    {
      // Extract Enums from String
      string[] splittedEnums = null;
      try
      {
        splittedEnums = title.Split(" ")[0].Split("-to-");
        if (splittedEnums.Length != 2)
          throw new Exception("Invalid Title");
      }
      catch
      {
        return new MapType[0];
      }

      // Try to Convert
      bool sourceSuccess = Enum.TryParse(typeof(MapType), splittedEnums[0], true, out object enumSource);
      bool destinationSuccess = Enum.TryParse(typeof(MapType), splittedEnums[1], true, out object enumDestination);

      if (sourceSuccess && destinationSuccess)
      {
        return [(MapType)enumSource, (MapType)enumDestination];
      }

      // No Success
      return new MapType[0];
    }

    public List<long> FromSeedsToLocations(long? overrideSeed = null)
    {
      List<long> currentSet = new List<long>();

      if (overrideSeed.HasValue)
      {
        currentSet.Add(overrideSeed.Value);
      }
      else
      {
        currentSet.AddRange(Seeds);
      }

      for (int i = 1; i < MapOrder.Length; i++)
      {
        MapType from = MapOrder[i - 1];
        MapType to = MapOrder[i];

        for (int j = 0; j < currentSet.Count; j++)
        {
          currentSet[j] = FromSourceToDestination(currentSet[j], from, to);
        }
      }

      return currentSet;
    }

    public long FromSeedRangeToMinLocation()
    {
      long minLocation = long.MaxValue;

      for(int i = 0; i < Seeds.Count; i+=2)
      {
        long smaller = Seeds[i];
        long bigger = Seeds[i] + Seeds[i + 1] - 1;

        for (long j = smaller; j < bigger; j++)
        {
          var x = FromSeedsToLocations();
          long testValue = FromSeedsToLocations(j).First();

          if(minLocation > testValue)
            minLocation = testValue;

          Console.WriteLine($"Teste Seed {j} -> {testValue}");
        }
      }

      return minLocation;
    }

    private long FromSourceToDestination(long input, MapType from, MapType to)
    {
      // Filter possible Mappings
      var possibleMappings = Maps.Where(m => m.SourceType == from && m.DestinationType == to);

      // Berechnung
      foreach (var possibleMapping in possibleMappings)
      {
        if (input < possibleMapping.SourceStart || input > possibleMapping.SourceStart + possibleMapping.Range)
          continue;

        return possibleMapping.DestinationStart + (input - possibleMapping.SourceStart);
      }

      // Nichts gefunden -> Input zurückgeben
      return input;
    }

    // TODO: Teil 2 ferig bearbeiten
    private long GetEndOfSamePath(long input, MapType current)
    {
      MapType from = current;
      MapType to = (current == MapType.Location) ? MapType.Location : MapOrder[MapOrder.ToList().IndexOf(current)];

      // Filter possible Mappings
      var possibleMappings = Maps.Where(m => m.SourceType == from && m.DestinationType == to);

      // Berechnung
      foreach (var possibleMapping in possibleMappings)
      {
        if (input < possibleMapping.SourceStart || input > possibleMapping.SourceStart + possibleMapping.Range)
          continue;

        // Tiefer reingehen, bis 
        return possibleMapping.DestinationStart + (input - possibleMapping.SourceStart);
      }
      return -1;
    }

  }
}
