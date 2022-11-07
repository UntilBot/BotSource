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
	private readonly IServiceProvider services;

	public DiscordSocketClient Client { get; }
	public InteractionService Interaction { get; }
	public EmoteService Emote { get; }
	public GameService Game { get; }

	public Until(string configPath)
	{
		Config config = JsonSerializer.Deserialize<Config>(
			File.ReadAllText(configPath, Encoding.UTF8));
		this.config = config;

		this.Client = new(new()
		{
			GatewayIntents =
				GatewayIntents.Guilds |
				GatewayIntents.GuildEmojis,
			HandlerTimeout = null,
			UseInteractionSnowflakeDate = false
		});
		this.Interaction = new(this.Client);
		this.Emote = new();
		this.Game = new();

		this.services = new ServiceCollection()
			.AddSingleton(this.Client)
			.AddSingleton(this.Interaction)
			.AddSingleton(this.Emote)
			.AddSingleton(this.Game)
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
		SocketInteractionContext<T> context = new(Client, interaction);
		await Interaction.ExecuteCommandAsync(context, services);
	}

	private async Task ReadyHandler()
	{
		await Emote.LoadEmotes(Client, this.config.EmoteGuilds);
		await Interaction.AddModulesAsync(
			typeof(Until).Assembly, this.services);
#if DEBUG
		await Interaction.RegisterCommandsToGuildAsync(
			this.config.DebugGuild, true);
#else
        await this.interaction.RegisterCommandsGloballyAsync(true);
#endif
	}

	public async Task MainAsync()
	{
		Client.Log += Log;

		await Client.LoginAsync(TokenType.Bot, this.config.Token);
		await Client.StartAsync();

		Client.SlashCommandExecuted += ExecuteInteractionAsync;
		Client.ButtonExecuted += ExecuteInteractionAsync;
		Client.SelectMenuExecuted += ExecuteInteractionAsync;
		Client.ModalSubmitted += ExecuteInteractionAsync;

		Client.Ready += ReadyHandler;

		await Task.Delay(-1);
	}
}
