using Discord;
using Discord.WebSocket;

namespace Until.Services;

public class EmoteService
{
	private List<GameEmote> emojis;

	public GameEmote GetEmote(string name) =>
		this.emojis.FirstOrDefault(e => e.Name == name);

	public async Task LoadEmotes(DiscordSocketClient client, ulong[] guilds)
	{
		Log("Emotes loading");
		this.emojis = new();
		foreach (ulong guild in guilds)
		{
			IReadOnlyCollection<GuildEmote> emotes =
				await client.GetGuild(guild).GetEmotesAsync();
			foreach (GuildEmote emote in emotes)
				this.emojis.Add(
					await GameEmote.Parse(emote).DownloadAsync());
		}
		Log("Emotes loaded");
	}

	private void Log(in string msg) =>
		Console.WriteLine(
			$"{DateTime.Now.ToString("HH:mm:ss")} {"Service",-12}{msg}");

	public EmoteService() { }
}
