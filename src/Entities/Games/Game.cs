namespace Until;

public abstract class Game
{
	protected List<IPlayer> Players { get; }

	public int Id { get; set; }
	public ulong ChannelId { get; }

	public Game(ulong channelId)
	{
		this.Players = new();

		this.ChannelId = channelId;
	}

	public IPlayer GetPlayer(ulong playerId) =>
		this.Players.SingleOrDefault(p => p.Id == playerId);

	public IPlayer GetPlayer(in byte index) =>
		this.Players.ElementAt(index);

	public void AddPlayer(IPlayer player) =>
		this.Players.Add(player);
}
