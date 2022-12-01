using Discord.Interactions;

namespace Until.Commands;

public class Sequence : InteractionModuleBase
{
	public async Task Run()
	{
		await RespondAsync("WIP");
	}
}
