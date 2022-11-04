using Until.Services;

namespace Until.Helpers;

public static class FrenchCards
{
	public static Dictionary<char, string> Faces => new()
	{
		{ 'A', "Ace" },
		{ '2', "Two" },
		{ '3', "Three" },
		{ '4', "Four" },
		{ '5', "Five" },
		{ '6', "Six" },
		{ '7', "Seven" },
		{ '8', "Eight" },
		{ '9', "Nine" },
		{ 'T', "Ten" },
		{ 'J', "Jack" },
		{ 'Q', "Queen" },
		{ 'K', "King" },
		{ 'X', "Joker" }
	};

	public static Dictionary<char, string> Suits => new()
	{
		{ 'C', "Clubs" },
		{ 'D', "Diamonds" },
		{ 'H', "Hearts" },
		{ 'S', "Spades" },
		{ 'X', "Joker" }
	};

	public static IEnumerable<FrenchCard> GetDeck(
		in EmoteService emoteService) =>
			GetDeck(0, emoteService);

	public static IEnumerable<FrenchCard> GetDeck(
		byte jokerCount, EmoteService emoteService)
	{
		foreach (char face in Faces.Keys.Take(13))
			foreach (char suit in Suits.Keys.Take(4))
				yield return FrenchCard.Parse($"{face}{suit}", emoteService);

		for (byte i = 0; i < jokerCount; i++)
			yield return FrenchCard.Parse("XX", emoteService);
	}

	public static FrenchCard GetRandomCard(EmoteService emoteService)
	{
		Random r = new();
		char face = Faces.Keys.ElementAt(r.Next(Faces.Count));
		char suit = Suits.Keys.ElementAt(r.Next(Suits.Count));
		return FrenchCard.Parse($"{face}{suit}", emoteService);
	}
}
