using System;
using System.Threading.Tasks;
using PyTaskBot.App.Bot.Wrappers;
using PyTaskBot.Infrastructure;
using Telegram.Bot.Types;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListTaskInCategoryCommand:Command
    {
        private PyTaskDatabase db;
        private CatListWrapper wrapper;
        public ListTaskInCategoryCommand(PyTaskDatabase db) : base("catinfo", "show list of tasks in category")
        {
            this.wrapper =new CatListWrapper(db);
            this.db = db;
            foreach (var x in db.GetCategories())
            {
               Aliases.Add(x);
            }
        }

        public override string CreateResponse(string query)
        {
            return wrapper.GetWrapped(query);
        }
    }
}