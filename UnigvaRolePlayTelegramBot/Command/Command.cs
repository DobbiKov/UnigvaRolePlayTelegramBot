using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UnigvaRolePlayTelegramBot.Command
{
    public abstract class Command
    {
        public abstract string[] Names { get; set; }

        public abstract void Execute(Message message, TelegramBotClient client);

        public virtual bool Contains(string command)
        {
            foreach (var comm in Names)
            {
                if (command.Contains(comm)) return true;
            }
            return false;
        }
    }
}
