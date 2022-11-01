using SkiaSharp;
using Discord;

namespace Until;

public class GameEmote : IEmote
{
	public ulong Id { get; }
	public string Name { get; }
	public string Url { get; }
	public bool Animated { get; }
	public SKBitmap Image { get; private set; }

	public GameEmote(ulong id, string name, string url, bool animated)
	{
		this.Id = id;
		this.Name = name;
		this.Url = url;
		this.Animated = animated;
	}

	public static GameEmote Parse(in Emote emote) =>
		new(emote.Id, emote.Name, emote.Url, emote.Animated);

	public async Task<GameEmote> DownloadAsync()
	{
		using (HttpClient httpClient = new())
		{
			byte[] bytes = await httpClient.GetByteArrayAsync(this.Url);
			using (MemoryStream ms = new(bytes))
			{
				this.Image = SKBitmap.Decode(ms).Resize(
					new SKImageInfo(64, 64),
					SKFilterQuality.Low);
			}
		}
		return this;
	}

	public override string ToString() =>
		$"<{(Animated ? "a" : "")}:{Name}:{Id}>";
}
