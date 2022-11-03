using Until.Services;

namespace Until;

public class FrenchCard : ICard
{
    private string face;
    private string suit;

    public string Name { get; }
    public GameEmote Emote { get; }

    // public FrenchCard(string name, GameEmote emote)
    // {
    //     this.Name = name;
    //     this.Emote = emote;
    // }

    public FrenchCard(string face, string suit, EmoteService emoteService)
    {
        this.face = face;
        this.suit = suit;
        this.Name = $"{face} of {suit}";
        this.Emote = emoteService.GetEmote($"{face}_of_{suit}".ToLower());
    }

    public static FrenchCard Parse(in string code, EmoteService emoteService) =>
        new FrenchCard(Faces[code[0]], Suits[code[1]], emoteService);

    public static readonly Dictionary<char, string> Faces = new()
    {
        { 'A', "Ace" },
        { '2', "Two" },
        { '3', "Three" },
        { '4', "Four" },
        { '5', "Five" },
        { '6', "Six" },
        { '7', "Seven" },
        { '8', "Eight" },
        { '9', "Nine" },
        { 'T', "Ten" },
        { 'J', "Jack" },
        { 'Q', "Queen" },
        { 'K', "King" }
    };

    public readonly static Dictionary<char, string> Suits = new()
    {
        { 'C', "Clubs" },
        { 'D', "Diamonds" },
        { 'H', "Hearts" },
        { 'S', "Spades" }
    };
}
