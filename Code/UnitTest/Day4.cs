using AdventOfCode2023.Day3;
using AdventOfCode2023.Day4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
  [TestClass]
  public class Day4
  {
    [TestMethod]
    public void Star1()
    {
      // Expected
      int expected = 13;

      // Setup Cards
      CardHolder cardHolder = new CardHolder(@"Input\Example\day-04.txt");

      // Summe aus Part Numbers
      int sumOfPoints = cardHolder.Cards.Sum(c => c.GetPoints());

      Assert.AreEqual(expected, sumOfPoints);
    }

    [TestMethod]
    public void Star2()
    {
      // Expected
      int expected = 30;

      // Setup Cards
      CardHolder cardHolder = new CardHolder(@"Input\Example\day-04.txt");

      // Summe
      long sumOfCards = cardHolder.CalculateAmountOfCards();

      Assert.AreEqual(expected, sumOfCards);
    }

    [TestMethod]
    public void Star2_RealData() 
    {
      // Expected
      long expected = 0b10101100111100110101000;

      // Setup Cards
      CardHolder cardHolder = new CardHolder(@"Input\day-04.txt");

      // Summe
      long sumOfCards = cardHolder.CalculateAmountOfCards();

      Assert.AreEqual(expected, sumOfCards);
    }
  }
}
