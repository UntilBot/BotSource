namespace Until;

public abstract class Game
{
	private readonly List<IPlayer> players;

	public int Id { get; set; }
	public ulong ChannelId { get; }

	public Game(ulong channelId)
	{
		this.players = new();

		this.ChannelId = channelId;
	}

	public IPlayer GetPlayer(ulong playerId) =>
		this.players.SingleOrDefault(p => p.Id == playerId);

	public IPlayer GetPlayer(in byte index) =>
		this.players.ElementAt(index);

	public void AddPlayer(IPlayer player) =>
		this.players.Add(player);
}
