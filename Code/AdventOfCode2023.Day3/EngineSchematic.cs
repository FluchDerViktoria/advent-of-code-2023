using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day3
{
  public class EngineSchematic
  {
    private string[] _data;
    public EngineSchematic(string filePath)
    {
      _data = File.ReadAllLines(filePath);
    }

    public EngineSchematic(string[] data)
    {
      _data = data;
    }

    public List<int> GetAllPartNumbers()
    {
      List<int> result = new List<int>();
      var regex = new Regex(@"\d+");

      for (int i = 0; i < _data.Length; i++)
      {
        foreach (Match match in regex.Matches(_data[i]))
        {
          if (IsPartNumber(i, match.Index, match.Value.Length))
          {
            result.Add(int.Parse(match.Value));
          }
        }
      }

      return result;
    }

    private bool IsPartNumber(int lineIndex, int indexOfFirstDigit, int numberLength)
    {
      // Symbol-Regex (genauer gesagt "Alles, was kein Punkt, Leerzeichen oder Zahl ist"-Regex)
      var regex = new Regex(@"[^\d\s\.]");

      // Default Start und Länge des Substrings, wenn Zahl nicht am Rand ist (Zahl selbst + jeweils eins mehr an den Enden)
      int substringStart = indexOfFirstDigit - 1;
      int substringLength = numberLength + 2;

      // Start und Länge anpassen, falls Zahl am Anfang vom String ist.
      if (substringStart == -1)
      {
        substringStart++;
        substringLength--;
      }

      // Länge anpassen, falls Zahl am Ende vom String steht (Gegeben: Jede Zeile von _data ist gleich lang)
      if (substringStart + substringLength > _data[0].Length)
      {
        substringLength = _data[0].Length - substringStart;
      }

      // Überpruft sowohl Zeile in lineIndex als auch die Zeile davor und danach (falls vorhanden) auf Symbole,
      // die sich um die Zahl befinden könnten. Math.Min und Math.Max wird hier genutzt, um Grenzwerten vorbeugen zu können.
      for (int i = Math.Max(lineIndex - 1, 0); i < Math.Min(lineIndex + 2, _data.Length); i++)
      {
        if (regex.Match(_data[i].Substring(substringStart, substringLength)).Success)
        {
          return true;
        }
      }


      // Keine Symbole gefunden -> Eindeutig keine Teilnummer
      return false;
    }

    public List<int> GetAllGearRatios()
    {
      List<int> result = new List<int>();

      // Regex
      var gearRegex = new Regex(@"\*");

      for (int i = 0; i < _data.Length; i++)
      {
        foreach (Match match in gearRegex.Matches(_data[i]))
        {
          List<int> ints = GetAdjacentPartNumber(i, match.Index);

          if (ints.Count != 2)
            continue;

          result.Add(ints[0] * ints[1]);
        }
      }

      return result;
    }

    public List<int> GetAdjacentPartNumber(int lineIndex, int gearIndex)
    {
      List<int> result = new List<int>();

      // Zahlen Regex
      var numberRegex = new Regex(@"\d+");

      // Überpruft sowohl Zeile in lineIndex als auch die Zeile davor und danach (falls vorhanden) auf Symbole,
      // die sich um die Zahl befinden könnten. Math.Min und Math.Max wird hier genutzt, um Grenzwerten vorbeugen zu können.
      for (int i = Math.Max(lineIndex - 1, 0); i < Math.Min(lineIndex + 2, _data.Length); i++)
      {
        foreach (Match match in numberRegex.Matches(_data[i]))
        {
          if (IsNumberAdjacentToGear(gearIndex,match.Index, match.Value.Length))
          {
            result.Add(int.Parse(match.Value));
          }
        }
      }
      return result;
        
    }

    private bool IsNumberAdjacentToGear(int gearIndex, int indexOfFirstDigit, int numberLength)
    {
      int indexOfLastDigit = indexOfFirstDigit + numberLength - 1;
      return !((indexOfFirstDigit < gearIndex - 1 && indexOfLastDigit < gearIndex - 1) || (indexOfLastDigit > gearIndex + 1 && indexOfFirstDigit > gearIndex + 1));
    }
  }
}
