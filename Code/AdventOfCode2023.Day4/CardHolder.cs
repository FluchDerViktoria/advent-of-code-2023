using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day4
{
  public class CardHolder
  {
    public List<Card> Cards { get; set; }

    public CardHolder(string path) 
    {
      Cards = Card.LoadCards(path);
    }

    public long CalculateAmountOfCards()
    {
      // Int1: Card-ID, Int2: AmountOfCards
      Dictionary<int, long> amountOfCards = new Dictionary<int, long>();

      // Fülle Dictionary mit allen Losen und Standard-Wert (1)
      Cards.ForEach(card =>
      {
        amountOfCards[card.ID] = 1;
      });

      // Berechnen und "Kopieren" der nächsten Lose
      for (int i = amountOfCards.Keys.Min(); i <= amountOfCards.Keys.Max(); i++)
      {
        // Los aus ID ermitteln
        Card card = Cards.FirstOrDefault(c => c.ID == i);

        // Null-Check
        if (card == null)
          continue;

        // Berechnen der Treffer
        int hits = card.GetHits();

        // Wiederhole so oft, wie es Lose gibt
        for (int j = 0; j < amountOfCards[i]; j++)
        {
          // "Kopieren"
          AddAmountToCards(i, hits, amountOfCards);
        }

      }

      return amountOfCards.Sum(a => a.Value);
    }

    private void AddAmountToCards(int currentCard, int matchResult, Dictionary<int, long> amountOfCards)
    {
      for (int i = 0; i < matchResult/* && currentCard + i + 1 < amountOfCards.Count()*/; i++)
      {
        amountOfCards[currentCard + i + 1]++;
      }
    }

  }
}
