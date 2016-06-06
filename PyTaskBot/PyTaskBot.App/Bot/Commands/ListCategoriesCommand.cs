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

        public override string CreateResponse(string query)
        {
           return string.Join("\n", db.GetCategories());
        }

        
    }
}