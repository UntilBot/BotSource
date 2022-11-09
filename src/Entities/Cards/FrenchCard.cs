using Until.Helpers;
using Until.Services;

namespace Until;

public class FrenchCard : ICard
{
	public string Name { get => $"{Face} of {Suit}"; }
	public GameEmote Emote { get; }

	public string Face { get; }
	public string Suit { get; }

	public FrenchCard(string face, string suit, GameEmote emote)
	{
		this.Emote = emote;

		this.Face = face;
		this.Suit = suit;
	}

	public static FrenchCard Parse(in string code, EmoteService emoteService)
	{
		string face = FrenchCards.Faces[code[0]];
		string suit = FrenchCards.Suits[code[1]];
		return new FrenchCard(
			face, suit,
			emoteService.GetEmote($"{face}_of_{suit}".ToLower()));
	}
}
