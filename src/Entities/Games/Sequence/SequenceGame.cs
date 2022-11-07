using Until.GameEnums.Sequence;
using Until.Helpers;
using Until.Extensions;
using Until.Services;

namespace Until;

public class SequenceGame : Game
{
	private readonly EmoteService emote;

	private SequenceTable table;
	private List<FrenchCard> deck;

	public GameStatus Status { get; set; }
	public byte Turn { get; set; }

	public SequenceGame(
		ulong channelId, ulong userId, EmoteService emoteService)
		: base(channelId)
	{
		this.emote = emoteService;

		this.table = new(emoteService);
		this.deck = FrenchCards.GetDeck(0, emoteService).Times(2).ToList();

		this.Status = GameStatus.Join;
		this.Turn = 0;

		AddPlayer(new SequencePlayer(userId));
	}
}
