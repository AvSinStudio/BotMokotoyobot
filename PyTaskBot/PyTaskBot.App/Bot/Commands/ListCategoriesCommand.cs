using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class ListCategoriesCommand : Command
    {
        private readonly PyTaskDatabase db;

        public ListCategoriesCommand(PyTaskDatabase db) : base("catlist", "get a list of categories names")
        {
            this.db = db;
        }

        public override string CreateResponse(string[] args)
        {
            return string.Join("\n", db.GetCategories());
        }
    }
}