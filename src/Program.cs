namespace Until;

public class Program
{
	public static void Main() =>
		new Until("config.json")
			.MainAsync().GetAwaiter().GetResult();

	private Program() { }
}
