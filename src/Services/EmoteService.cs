using Discord;
using Discord.WebSocket;

namespace Until.Services;

public class EmoteService
{
	private List<UntilEmote> emojis;

	public UntilEmote GetEmote(string name) =>
		this.emojis.FirstOrDefault(e => e.Name == name);

	public async Task LoadEmojis(DiscordSocketClient client, ulong[] guilds)
	{
		this.emojis = new();
		foreach (ulong guild in guilds)
			foreach (GuildEmote emote in await client.GetGuild(guild).GetEmotesAsync())
				this.emojis.Add(new UntilEmote(emote));
	}

	public EmoteService() { }
}
