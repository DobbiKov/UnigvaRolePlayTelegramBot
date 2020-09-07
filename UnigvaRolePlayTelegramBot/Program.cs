using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using UnigvaRolePlayTelegramBot.Command.Commands;

namespace UnigvaRolePlayTelegramBot
{
    public class Program
    {
        private static TelegramBotClient client;
        private static List<Command.Command> commandsList;
        public static void Main(string[] args)
        {
            BotSetting.users = new List<Models.User>();
            client = new TelegramBotClient(BotSetting.Token) { Timeout = TimeSpan.FromSeconds(5) };
            commandsList = new List<Command.Command>();

            commandsList.Add(new GetAllAccsCommand());
            commandsList.Add(new GetMyAccCommand());
            commandsList.Add(new GetMyIdCommand());
            client.OnMessage += OnChatMessage;
            client.StartReceiving();
            CreateHostBuilder(args).Build().Run();
        }

        private static void OnChatMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                var message = e.Message;

                foreach (var command in commandsList)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(message, client);
                        break;
                    }
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
