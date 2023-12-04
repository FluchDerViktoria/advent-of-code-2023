using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Day4
{
  internal class Program
  {
    static void Main(string[] args)
    {
      // Title
      Console.WriteLine("Code of Advent - Türchen 4\n");

      // Karten Laden
      CardHolder cardHolder = new CardHolder(@"Input\day-04.txt");

      // Calculating
      int sumOfPoints = cardHolder.Cards.Sum(c => c.GetPoints());
      long amountCards = cardHolder.CalculateAmountOfCards();

      // Ausgabe
      Console.WriteLine($"Summe aller Punkte: {sumOfPoints}");
      Console.WriteLine($"Anzahl gewonnener (und vorhandener) Lose: {amountCards}"); // Not 5165100 , 25474650
    }
  }
}
