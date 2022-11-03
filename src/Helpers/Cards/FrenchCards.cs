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
		{ 'K', "King" }
	};

	public static Dictionary<char, string> Suits => new()
	{
		{ 'C', "Clubs" },
		{ 'D', "Diamonds" },
		{ 'H', "Hearts" },
		{ 'S', "Spades" }
	};

	public static IEnumerable<FrenchCard> GetDeck(EmoteService emoteService) =>
		Faces.Keys.SelectMany(
			face => Suits.Keys.Select(
				suit => FrenchCard.Parse($"{face}{suit}", emoteService)));

	public static FrenchCard GetRandomCard(EmoteService emoteService)
	{
		Random r = new();
		char face = Faces.Keys.ElementAt(r.Next(Faces.Count));
		char suit = Suits.Keys.ElementAt(r.Next(Suits.Count));
		return FrenchCard.Parse($"{face}{suit}", emoteService);
	}
}
