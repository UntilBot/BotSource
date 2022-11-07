using System.Diagnostics;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Until.Helpers;

namespace Until.Commands;

[Group("dev", "[Dev] Developer commands")]
[RequireOwner]
public class Dev : InteractionModuleBase
{
	public DiscordSocketClient Client { get; set; }

	[SlashCommand("restart", "[DEV] Restart the bot")]
	public async Task Restart()
	{
		try
		{
			ProcessStartInfo process = new()
			{
				FileName = "/bin/bash",
				Arguments = $"-c \"sh start.sh\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};
			Process.Start(process);
			await RespondAsync(embed: Embeds.Info(
				"Restarting...",
				"This may take a while."));
			Environment.Exit(0);
		}
		catch (Exception e)
		{
			await RespondAsync(
				embed: Embeds.Error(
					"Error",
					$"Can't find bash at `/bin/bash`.\n" +
					$"```{e.Message}```"),
				ephemeral: true);
		}
	}

	[SlashCommand("shutdown", "[DEV] Shutdown the bot")]
	public async Task Shutdown()
	{
		await RespondAsync(embed: Embeds.Info(
			"Shutting down..."));
		Environment.Exit(0);
	}

	[SlashCommand("test", "[DEV] Testing")]
	public async Task Test() =>
		await RespondAsync(
			Context.User.Mention,
			allowedMentions: AllowedMentions.None);
}
