namespace Until;

public class Config
{
	public string Token { get; set; }
	public ulong DebugGuild { get; set; }
	public ulong[] EmoteGuilds { get; set; }

	public Config() { }
}
