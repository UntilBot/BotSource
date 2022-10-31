namespace Until;

public class Config
{
	public string Token { get; set; }
	public ulong DebugGuild { get; set; }
	public IReadOnlyCollection<ulong> EmojiGuilds { get; set; }

	public Config() { }
}
