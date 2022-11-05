using Discord;
using Until.GameEnums.Sequence;

namespace Until;

public class SequencePlayerColor
{
	public GameColor GameColor { get; }

	public Color DiscordColor
	{
		get
		{
			Dictionary<GameColor, Color> colors = new Dictionary<GameColor, Color>
				{
					{ GameColor.Red, new(0xbe1931) },
					{ GameColor.Green, new(0x4da631) },
					{ GameColor.Blue, new(0x445893) }
				};
			return colors[this.GameColor];
		}
	}

	public SequencePlayerColor(GameColor color) =>
		this.GameColor = color;

	public override string ToString() => GameColor switch
	{
		GameColor.Red => "🔴",
		GameColor.Green => "🟢",
		GameColor.Blue => "🔵",
		GameColor.Joker => "❔",
		_ => "⚫"
	};
}
