using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using DiscBot;
using Discord.WebSocket;

namespace DiscBot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("PingTest")
                .WithDescription("Descrição de Ping")
                .WithColor(Color.DarkBlue);
            await ReplyAsync("", false, builder.Build());

        }
        //Get in voice-chat
        [Command("colae", RunMode = RunMode.Async)]        
        public async Task getIn ()
        {
            SocketGuildUser user = Context.User as SocketGuildUser;
            if(user.VoiceState == null)
            {
                await ReplyAsync("Conecta num voice-chat ae mein ");
                return;
            }
            ISocketAudioChannel channel = user.VoiceChannel;
            Console.WriteLine(channel);
            await channel.ConnectAsync();
            
            Console.WriteLine("Conectado");
        }
        
        [Command("return")]
        public async Task RetornarBuildAsync()
        {

            EmbedBuilder builder = new EmbedBuilder();

            builder.AddField("um teste","muito loco")
                .AddInlineField("B","t")
                .AddInlineField("C","e");

        }

        [Command("responde")]
        public async Task RetornarFraseAsync(SocketGuildUser user)
        {
            //Deveria retornar a tag do usuario que chamou a função
            EmbedBuilder builder = new EmbedBuilder();

           await ReplyAsync($"O qq ce qué, {user.Mention }");

        }
        [Command("help")]
        public async Task HelpCommand()
        {
            EmbedBuilder builder = new EmbedBuilder();
            await ReplyAsync("Comandos :'colae' 'ping' ");
        }
    }
}
