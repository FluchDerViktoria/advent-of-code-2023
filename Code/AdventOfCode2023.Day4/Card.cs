using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day4
{
  public class Card
  {
    public int ID { get; private set; }
    public List<int> WinningNumbers { get; set; }
    public List<int> OwnNumbers { get; set; }

    public Card(int id)
    {
      ID = id;
      WinningNumbers = new List<int>();
      OwnNumbers = new List<int>();
    }

    public int GetPoints()
    {
      int hits = GetHits();

      // Sonderfall: Hits = 0
      if (hits == 0)
        return 0;

      // Gleichbedeutend mit 2 ^ (hits - 1).
      return 1 << (hits - 1);
    }

    public int GetHits()
    {
      int hits = 0;
      foreach (int ownNumber in OwnNumbers)
      {
        if (WinningNumbers.Contains(ownNumber))
          hits++;
      }

      return hits;
    }

    public static List<Card> LoadCards(string filePath)
    {
      List<Card> cards = new List<Card>();

      string[] lines = File.ReadAllLines(filePath);

      Regex numberRegex = new Regex(@"\d+");

      foreach (string line in lines)
      {
        // Teilen zwischen ID und Number-Arrays
        string[] splittedLine = line.Split(":");

        // ID Rausfiltern
        int id = int.Parse(numberRegex.Match(splittedLine[0]).Value);

        // Number-Arrays rausfiltern
        string[] numberArrayStrings = splittedLine[1].Split("|");

        // Card erstellen und zur Liste hinzufügen
        Card card = new Card(id)
        {
          WinningNumbers = ToIntList(numberArrayStrings[0]),
          OwnNumbers = ToIntList(numberArrayStrings[1])
        };

        cards.Add(card);

      }

      return cards;
    }

    private static List<int> ToIntList(string stringOfInts)
    {
      List<int> result = new List<int>();
      Regex numberRegex = new Regex(@"\d+");

      foreach (Match match in numberRegex.Matches(stringOfInts))
      {
        result.Add(int.Parse(match.Value));
      }

      return result;
    }
  }
}
