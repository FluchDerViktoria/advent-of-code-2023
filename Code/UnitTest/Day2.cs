using AdventOfCode2023.Day2;

namespace UnitTest
{
  [TestClass]
  public class Day2
  {
    [TestMethod]
    public void Star1()
    {
      // Max Cubes
      int maxCubeRed = 12;
      int maxCubeGreen = 13;
      int maxCubeBlue = 14;

      // Erwartete Antwort
      int expected = 8;

      List<Game> games = Game.LoadGamesFromFile(@"Input\Example\day-02.txt");

      int sumOfValidId = 0;

      games.ForEach(g =>
      {
        if(g.IsGameValid(maxCubeRed, maxCubeGreen, maxCubeBlue))
          sumOfValidId += g.ID;
      });

      Assert.AreEqual(expected, sumOfValidId);
    }

    [TestMethod]
    public void Star2()
    {
      // Erwartete Antwort
      int expected = 2286;

      List<Game> games = Game.LoadGamesFromFile(@"Input\Example\day-02.txt");

      int sumOfPower = 0;

      games.ForEach(g =>
      {
        sumOfPower += g.GetPower();
      });

      Assert.AreEqual(expected, sumOfPower);
    }
  }
}