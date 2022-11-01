namespace Until;

public abstract class Game
{
	public int Id { get; set; }
	public ulong ChannelId { get; }
	public List<IPlayer> Players { get; }

	public Game(ulong channelId)
	{
		this.ChannelId = channelId;
		this.Players = new();
	}

	public IPlayer GetPlayer(ulong playerId) =>
		this.Players.SingleOrDefault(p => p.Id == playerId);

	public IPlayer GetPlayer(in byte index) =>
		this.Players.ElementAt(index);
}
