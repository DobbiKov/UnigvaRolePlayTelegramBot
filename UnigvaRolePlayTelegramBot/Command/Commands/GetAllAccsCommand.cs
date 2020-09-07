using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UnigvaRolePlayTelegramBot.Command.Commands
{
    public class GetAllAccsCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "getallaccs" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, $"Tut.");
            using(var MyConn = new MySqlConnection("Datasource=151.80.47.109;Database=gs47333;User=gs47333;Password=8m36QM0o1d"))
            {
                MyConn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `accounts`", MyConn);
                /*var users = command.CommandText = "SELECT * FROM `accounts`";*/
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, $"{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}.");
                    while (reader.Read())
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, $"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}");
                    }
                }
                MyConn.Close();
            }
            await client.SendTextMessageAsync(message.Chat.Id, $"Tut2.");
        }
    }
}
