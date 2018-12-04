using System;
using Discord;
using System.IO;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DiscBot
{
    class Program
    {

        private DiscordSocketClient Client;
        private CommandService Commands;
        private IServiceProvider _services;
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        //SocketGuildUser (atributos e ações de usuario no discord)
        public async Task RunBotAsync()
        {

            Client = new DiscordSocketClient();
            Commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton(Commands)
                .BuildServiceProvider();

            string botToken = "";


            //event Subscriptions

            Client.Log += Log;
            //Client.UserJoined += AnnounceUserJoined;

            await RegisterCommandsAsync();
            // Loga no socket utilizando a token do Bot
            await Client.LoginAsync(TokenType.Bot, botToken);

            //Se conectando com os parametros do LoginAsync

            await Client.StartAsync();

            //comando looping para a aplicação não fechar após rodar o código
            await Task.Delay(-1);


        }

        /*  private async Task AnnounceUserJoined(SocketGuildUser user)
          {
              var guild = user.Guild;
              var channel = guild.DefaultChannel;
              await channel.SendMessageAsync($"welcome,{user.Mention}");
          }*/

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            //logs para mostrar no console
            return Task.CompletedTask;
        }
        public async Task RegisterCommandsAsync()
        {
            Client.MessageReceived += HandleCommandAsync;
            Console.WriteLine("public Async Task Register Command Async");
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

        }
        //Função de Comandos e configuração de Prefixo
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            Console.WriteLine(arg);
            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!lg ", ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(Client, message);

                var result = await Commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }

    }
}

