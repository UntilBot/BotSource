using Discord;
using SkiaSharp;
using Until.GameEnums.Sequence;
using Until.Services;

namespace Until;

public class SequenceTable
{
	private readonly SequenceTableCell[,] table;

	public SequenceTable(EmoteService emoteService)
	{
		this.table = new SequenceTableCell[10, 10];

		string[] defaultTable = new[]
		{
			"XX", "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "XX",
			"6C", "5C", "4C", "3C", "2C", "AH", "KH", "QH", "TH", "TS",
			"7C", "AS", "2D", "3D", "4D", "5D", "6D", "7D", "9H", "QS",
			"8C", "KS", "6C", "5C", "4C", "3C", "2C", "8D", "8H", "KS",
			"9C", "QS", "7C", "6H", "5H", "4H", "AH", "9D", "7H", "AS",
			"TC", "TS", "8C", "7H", "2H", "3H", "KH", "TD", "6H", "2D",
			"QC", "9S", "9C", "8H", "9H", "TH", "QH", "QD", "5H", "3D",
			"KC", "8S", "TC", "QC", "KC", "AC", "AD", "KD", "4H", "4D",
			"AC", "7S", "6S", "5S", "4S", "3S", "2S", "2H", "3H", "5D",
			"XX", "AD", "KD", "QD", "TD", "9D", "8D", "7D", "6D", "XX"
		};

		byte i = 0;
		for (byte y = 0; y < 10; y++)
			for (byte x = 0; x < 10; x++)
			{
				string cell = defaultTable[i++];
				this.table[x, y] = new()
				{
					Card = FrenchCard.Parse(cell, emoteService),
					GameColor = cell == "XX" ?
						GameColor.Joker :
						GameColor.None
				};
			}
	}

	public IEnumerable<string> AvaliableSuits() =>
		this.table
			.Cast<SequenceTableCell>()
			.Where(cell => cell.GameColor == GameColor.None)
			.Select(cell => cell.Card.Suit)
			.Distinct();

	public FileAttachment ToImage(in FrenchCard highlightedCard)
	{
		using (SKSurface surface = SKSurface.Create(new SKImageInfo(640, 766)))
		{
			SKCanvas canvas = surface.Canvas;
			canvas.Clear(SKColors.Transparent);

			string[] numbers = new[]
			{
				"one",
				"two",
				"three",
				"four",
				"five",
				"six",
				"seven",
				"eight",
				"nine"
			};

			byte i = 0;
			for (byte y = 0; y < 10; y++)
				for (byte x = 0; x < 10; x++)
				{
					SequenceTableCell cell = this.table[x, y];
					GameColor color = cell.GameColor;
					bool unclaimed =
						color == GameColor.None ||
						color == GameColor.Joker;
					
					
				}

			return new(
				surface
					.Snapshot()
					.Encode(SKEncodedImageFormat.Png, 100)
					.AsStream(),
				"Until-Sequence.png");
		}
	}
}
