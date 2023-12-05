using AdventOfCode2023.Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
  [TestClass]
  public class Day5
  {
    [TestMethod]
    public void Star1()
    {
      // Expected
      long expected = 35;

      // Setup Mapping
      MappingHelper mappingHelper = new MappingHelper(@"Input\Example\day-05.txt");

      // Minimum Location
      long location = mappingHelper.FromSeedsToLocations().Min();

      Assert.AreEqual(expected, location);
    }

    [TestMethod]
    public void Star2()
    {
      // Expected
      long expected = 46;

      // Setup Mapping
      MappingHelper mappingHelper = new MappingHelper(@"Input\Example\day-05.txt");

      // Minimum Location
      long location = mappingHelper.FromSeedRangeToMinLocation();

      Assert.AreEqual(expected, location);
    }
  }
}
