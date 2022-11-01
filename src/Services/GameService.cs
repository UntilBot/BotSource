namespace Until.Services;

public class GameService
{
	private readonly List<Game> games;

	public GameService()
	{
		this.games = new();
	}

	public Game GetGame(int gameId) =>
		this.games.SingleOrDefault(g => g.Id == gameId);

	public Game GetGame(ulong playerId, ulong channelId) =>
		this.games.SingleOrDefault(g =>
			g.GetPlayer(playerId) != null &&
			g.ChannelId == channelId);

	public Game AddGame(in Game game)
	{
		if (this.games.Count == 0)
			game.Id = 1;
		else
			game.Id = this.games.Max(g => g.Id) + 1;
		this.games.Add(game);
		return game;
	}

	public void RemoveGame(int gameId) =>
		this.games.RemoveAll(g => g.Id == gameId);
}
