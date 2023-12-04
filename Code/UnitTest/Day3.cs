using AdventOfCode2023.Day3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
  [TestClass]
  public class Day3
  {
    [TestMethod]
    public void Star1()
    {
      // Expected
      int expected = 4361;

      // Setup Engine
      EngineSchematic engineSchematic = new EngineSchematic(@"Input\Example\day-03.txt");

      // Summe aus Part Numbers
      int sumOfPartNumbers = engineSchematic.GetAllPartNumbers().Sum();

      Assert.AreEqual(expected, sumOfPartNumbers);
    }

    [TestMethod]
    public void Star2()
    {
      // Expected
      int expected = 467835;

      // Setup Engine
      EngineSchematic engineSchematic = new EngineSchematic(@"Input\Example\day-03.txt");

      // Summe
      int sumOfGearRatio = engineSchematic.GetAllGearRatios().Sum();

      Assert.AreEqual(expected, sumOfGearRatio);
    }
  }
}
