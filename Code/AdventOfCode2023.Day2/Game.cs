using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day2
{
  public class Game
  {
    #region Properties
    public int ID 
    { 
      get; 
      private set; 
    }

    public List<CubeSet> Sets { get; private set; }

    #endregion Properties

    #region Constructors

    public Game()
    {
      ID = -1;
      Sets = new List<CubeSet>();
    }

    public Game(int id) : this()
    {
      this.ID = id; 
    }

    #endregion Constructors

    #region Game Functions

    public void AddSet(int red, int green, int blue)
    {
      CubeSet cubeSet = new CubeSet();
      cubeSet.AmountRed = red;
      cubeSet.AmountGreen = green;
      cubeSet.AmountBlue = blue;

      Sets.Add(cubeSet);
    }

    public bool IsGameValid(int maxCubesRed, int maxCubesGreen, int maxCubesBlue)
    {
      foreach (CubeSet set in Sets)
      {
        // Wenn irgendwas größer als max ist, Schleife mit "false" verlassen
        if (set.AmountRed > maxCubesRed || set.AmountGreen > maxCubesGreen || set.AmountBlue > maxCubesBlue)
          return false;
      }

      // Nichts unauffälligesd gefunden -> Wird schon passen
      return true;
    }

    public int GetPower()
    {
      int minRedNeeded = 0;
      int minGreenNeeded = 0;
      int minBlueNeeded = 0;

      // Setze Mindestanzahl der Cubes, die für ein Spiel benötigt werden
      foreach (CubeSet set in Sets)
      {
        // Falls Amount größer ist als min, setzte min auf Amount
        minRedNeeded = Math.Max(minRedNeeded, set.AmountRed);
        minGreenNeeded = Math.Max(minGreenNeeded, set.AmountGreen);
        minBlueNeeded = Math.Max(minBlueNeeded, set.AmountBlue);
      }

      // Rückgabe -> Muliplizieren der Min-Amounts
      return minRedNeeded * minGreenNeeded * minBlueNeeded;
    }


    #endregion Game Functions

    #region Loading from File

    /// <summary>
    /// Enthält Logik, um Spiele aus dem Input rauszuextrahieren
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static List<Game> LoadGamesFromFile(string filePath)
    {
      List<Game> list = new List<Game>();

      // Lade Spiel pro Zeile
      string[] gamesAsLine = File.ReadAllLines(filePath);

      foreach (string line in gamesAsLine)
      {
        Game game = CreateGameFromLine(line);

        if(game != null)
        {
          list.Add(game);
        }
      }

      return list;
    }

    private static Game CreateGameFromLine(string line) 
    {
      // Game #: # green, # blue, # red; # blue, # green

      // Ungültige Line mit null verlassen
      if (!Regex.Match(line, @"Game\s\d+:").Success)
        return null;

      // ID Rausfiltern, indem bei : geteilt wird
      // 0: "Game #"; 1: Tatsächliches Spiel 
      string[] gameSplit = line.Split(':');

      int id = int.Parse(Regex.Match(gameSplit[0], @"\d+").Value);

      // Leeres Spiel mit ID erstellen
      Game game = new Game(id);

      // Sets aufdröseln mit Split nach ";"
      string[] setStrings = gameSplit[1].Split(";");

      // Alle Sets hinzufügen
      foreach (string s in setStrings)
      {
        game.AddSetFromString(s);
      }

      // Return Game
      return game;
    }

    private void AddSetFromString(string setString)
    {

      // Regex zur Zahlen Extraktion
      Regex intRegex = new Regex(@"\d+");


      // Variablen vorbereiten
      int red = 0;
      int green = 0;
      int blue = 0;

      // Extrahiere Rot
      var redMatch = Regex.Match(setString, @"\d+ red");
      if (redMatch.Success)
      {
        red = int.Parse(intRegex.Match(redMatch.Value).Value);
      }

      // Extrahiere Grün
      var greenMatch = Regex.Match(setString, @"\d+ green");
      if (greenMatch.Success)
      {
        green = int.Parse(intRegex.Match(greenMatch.Value).Value);
      }

      // Extrahiere Blau
      var blueMatch = Regex.Match(setString, @"\d+ blue");
      if (blueMatch.Success)
      {
        blue = int.Parse(intRegex.Match(blueMatch.Value).Value);
      }

      // Falls nichts geparst wurde, nicht hinzufügen
      if (!redMatch.Success && !greenMatch.Success && !blueMatch.Success)
        return;

      // Set hinzufügen
      this.AddSet(red, green, blue);
    }

    #endregion Loading from File
  }
}
