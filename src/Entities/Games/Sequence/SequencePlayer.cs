using Until.GameEnums.Sequence;

namespace Until;

public class SequencePlayer : IPlayer
{
	private List<FrenchCard> hand;

	public ulong Id { get; }
	public SequencePlayerColor Color { get; set; }

	public byte HandCount =>
		(byte)this.hand.Count;

	public SequencePlayer(ulong id)
	{
		this.hand = new();

		this.Id = id;
		this.Color = new(GameColor.None);
	}

	public void AddCard(FrenchCard card) =>
		this.hand.Add(card);
}
