using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UnigvaRolePlayTelegramBot.Command.Commands
{
    public class GetMyAccCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "getmyacc", "get_my_acc" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var finded = false;
            using (var MyConn = new MySqlConnection("Datasource=151.80.47.109;Database=gs47333;User=gs47333;Password=8m36QM0o1d"))
            {
                MyConn.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM `accounts` WHERE `pTelegramId` = '{message.From.Id}'", MyConn);
                /*var users = command.CommandText = "SELECT * FROM `accounts`";*/
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        finded = true;
                        var id = reader.GetValue(0);
                        var nick = reader.GetValue(1);
                        var pCash = reader.GetValue(6);
                        var pBCash = reader.GetValue(6);
                        var pLVL = reader.GetValue(11);
                        var pEXP = reader.GetValue(21);
                        await client.SendTextMessageAsync(message.Chat.Id, $"Id аккаунта: {id}");
                        await client.SendTextMessageAsync(message.Chat.Id, $"NickName: {nick}");
                        await client.SendTextMessageAsync(message.Chat.Id, $"Денег наличными: {pCash}");
                        await client.SendTextMessageAsync(message.Chat.Id, $"Денег на банковском счету: {pBCash}");
                        await client.SendTextMessageAsync(message.Chat.Id, $"Уровень: {pLVL}[{pEXP}/{(int)pLVL*4}]");
                    }
                }
                if(finded == false)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, $"Мы не нашли ваш аккаунт. :(");
                }
                MyConn.Close();
            }
        }
    }
}
