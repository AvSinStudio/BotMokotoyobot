using System;
using System.Threading.Tasks;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListCategoriesCommand:Command
    {
        private PyTaskDatabase db;
        public ListCategoriesCommand(PyTaskDatabase db) : base("catlist", "get a list of categories names")
        {
            this.db = db;
        }

        public override void Execute(string query, long id, Func<long, string, Task<Message>> sender)
        {
            sender(id, string.Join("\n", db.GetListOfCategories()));
        }
    }
}