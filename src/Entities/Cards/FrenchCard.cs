using Until.Helpers;
using Until.Services;

namespace Until;

public class FrenchCard : ICard
{
	private string face;
	private string suit;

	public string Name { get; }
	public GameEmote Emote { get; }

	public FrenchCard(string face, string suit, GameEmote emote)
	{
		this.face = face;
		this.suit = suit;
		this.Name = $"{face} of {suit}";
		this.Emote = emote;
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
