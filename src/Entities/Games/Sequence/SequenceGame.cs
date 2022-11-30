using Discord;
using Until.GameEnums.Sequence;
using Until.Helpers;
using Until.Extensions;
using Until.Services;

namespace Until;

public class SequenceGame : Game
{
	// private readonly EmoteService emote;
	private readonly SequenceTable table;
	private readonly List<FrenchCard> deck;
	private byte turn;

	public GameStatus Status { get; set; }

	public SequencePlayer CurrentPlayer =>
		GetPlayer(turn) as SequencePlayer;

	public SequenceGame(
		ulong channelId, ulong userId, EmoteService emoteService)
		: base(channelId)
	{
		// this.emote = emoteService;
		this.table = new(emoteService);
		this.deck = FrenchCards.GetDeck(0, emoteService).Times(2).ToList();
		this.turn = 0;

		this.Status = GameStatus.Join;

		AddPlayer(new SequencePlayer(userId));
	}

	public byte CountCardOnTable(string cardName) =>
		this.table.CountCard(cardName);

	public void FillPlayerHands()
	{
		foreach (SequencePlayer player in Players.Cast<SequencePlayer>())
		{
			byte cardCount = 0;
			while (cardCount < 7)
				player.AddCard(PullCard());
		}
	}

	public FileAttachment GetTableImage() => GetTableImage(null);
	public FileAttachment GetTableImage(in FrenchCard highlightedCard) =>
		this.table.ToImage(highlightedCard);

	public void NextPlayer() =>
		this.turn = (byte)(++this.turn % Players.Count);

	public void PlaceChip(in FrenchCard card, in byte index)
	{
		this.table.PlaceChip(
			CurrentPlayer.Color.GameColor,
			card.Name,
			index);
		CurrentPlayer.RemoveCard(card);
		CurrentPlayer.AddCard(PullCard());
	}

	private FrenchCard PullCard()
	{
		Random r = new();
		byte index = (byte)r.Next(0, this.deck.Count);
		FrenchCard card = this.deck[index];
		this.deck.RemoveAt(index);
		return card;
	}
}
