using SkiaSharp;
using Discord;

namespace Until;

public class UntilEmote : IEmote
{
	public ulong Id { get; }
	public string Name { get; }
	public string Url { get; }
	public bool Animated { get; }
	public SKBitmap Image { get; }

	public UntilEmote(GuildEmote emote)
	{
		this.Id = emote.Id;
		this.Name = emote.Name;
		this.Url = emote.Url;
		this.Animated = emote.Animated;
	}

	public override string ToString() =>
		$"<{(Animated ? "a" : "")}:{Name}:{Id}>";
}
