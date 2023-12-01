using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day1
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // Title
      Console.WriteLine("Code of Advent - Türchen 1\n");

      // Input rauslesen
      string[] inputs = File.ReadAllLines(@"Input\day-01.txt");

      // Zahlen rausextrahieren und alles zusammenaddieren
      long sum = 0;
      foreach (string input in inputs)
      {
        sum += GetTwoDigitNumber(input);
      }

      // Summe ausgeben
      Console.WriteLine("Calibration value: " + sum);
    }

    /// <summary>
    /// Extrahiert die zweistellige Zahl aus dem Input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static int GetTwoDigitNumber(string input)
    {
      // Convert Spelled out Digits
      string output = FromSpelledOutToDigit(input);

      // Remove all letters
      output = Regex.Replace(output, @"\D", "");

      // Only save first and last number
      output = !string.IsNullOrEmpty(output) ? output.First().ToString() + output.Last().ToString() : "00";

      // Return as int
      return int.Parse(output);
    }

    /// <summary>
    /// Konvertiert die erste und die letzte in Worten ausgeschriebene Zahl in eine tatsächliche Zahl
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string FromSpelledOutToDigit(string input)
    {
      // Erste Zahl konvertieren
      string output = input;
      var x = new Regex(@"one|two|three|four|five|six|seven|eight|nine");
      output = x.Replace(output, Program.ReplacingMatch, 1);

      // Letzte Zahl konvertieren mit Hilfe von string.Reverse()-Hacks (geht sicher schöner, aber Hauptsache es funktioniert)
      output = new string(output.Reverse().ToArray());
      var seachLast = new Regex(@"enin|thgie|neves|xis|evif|ruof|eerht|owt|eno");
      output = seachLast.Replace(output, Program.ReplacingMatchReversed, 1);
      output = new string(output.Reverse().ToArray());

      return output;

    }

    #region Matching Methods

    public static string ReplacingMatch(Match m)
    {
      Dictionary<string, string> convertingDict = new Dictionary<string, string>()
      {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" },
      };

      return convertingDict[m.Value.ToLower()] + m.Value;
    }

    public static string ReplacingMatchReversed(Match m)
    {
      Dictionary<string, string> convertingDict = new Dictionary<string, string>()
      {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" },
      };
      string key = new String(m.Value.Reverse().ToArray());
      return convertingDict[key] + m.Value;
    }

    #endregion MAtching Functions
  }
}