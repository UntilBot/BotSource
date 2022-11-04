using Until.GameEnums.Sequence;

namespace Until;

public class SequencePlayer : IPlayer
{
	private List<FrenchCard> hand;

	public ulong Id { get; }
	public SequencePlayerColor Color { get; set; }

	public SequencePlayer(ulong id)
	{
		this.hand = new();

		this.Id = id;
		this.Color = new(GameEnums.Sequence.Color.None);
	}
}
