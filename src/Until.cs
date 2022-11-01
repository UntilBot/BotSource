using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Until.Services;

namespace Until;

public class Until
{
	private readonly Config config;
	private readonly DiscordSocketClient client;
	private readonly InteractionService interaction;
	private readonly EmoteService emote;
	private readonly GameService game;

	private readonly IServiceProvider services;

	public Until(string configPath)
	{
		Config config = JsonSerializer.Deserialize<Config>(
			File.ReadAllText(configPath, Encoding.UTF8));
		this.config = config;
		this.client = new(new()
		{
			GatewayIntents =
				GatewayIntents.Guilds |
				GatewayIntents.GuildEmojis,
			HandlerTimeout = null,
			UseInteractionSnowflakeDate = false
		});
		this.interaction = new(this.client);
		this.emote = new();
		this.game = new();

		this.services = new ServiceCollection()
			.AddSingleton(this.client)
			.AddSingleton(this.interaction)
			.BuildServiceProvider();
	}
	private Until() { }

	private Task Log(LogMessage message)
	{
		Console.WriteLine(message.ToString());
		return Task.CompletedTask;
	}

	private async Task ExecuteInteractionAsync<T>(T interaction)
		where T : SocketInteraction
	{
		SocketInteractionContext<T> context = new(client, interaction);
		await this.interaction.ExecuteCommandAsync(context, services);
	}

	private async Task ReadyHandler()
	{
		await this.emote.LoadEmotes(this.client, this.config.EmoteGuilds);
		await this.interaction.AddModulesAsync(
			typeof(Until).Assembly, this.services);
#if DEBUG
		await this.interaction.RegisterCommandsToGuildAsync(
			this.config.DebugGuild, true);
#else
        await this.interaction.RegisterCommandsGloballyAsync(true);
#endif
	}

	public async Task MainAsync()
	{
		client.Log += Log;

		await client.LoginAsync(TokenType.Bot, this.config.Token);
		await client.StartAsync();

		this.client.SlashCommandExecuted += ExecuteInteractionAsync;
		this.client.ButtonExecuted += ExecuteInteractionAsync;
		this.client.SelectMenuExecuted += ExecuteInteractionAsync;
		this.client.ModalSubmitted += ExecuteInteractionAsync;

		this.client.Ready += ReadyHandler;

		await Task.Delay(-1);
	}
}
