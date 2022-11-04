using Until.GameEnums.Sequence;

namespace Until;

public class SequencePlayerColor
{
	public Color Color { get; }

	public string ColorEmoji => Color switch
	{
		Color.Red => "🔴",
		Color.Green => "🟢",
		Color.Blue => "🔵",
		Color.Joker => "❔",
		_ => "⚫"
	};

	public SequencePlayerColor(Color color) =>
		this.Color = color;
}
