using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class ListCategoriesCommand : Command
    {
        private readonly PyTaskDatabase db;

        public ListCategoriesCommand(PyTaskDatabase db)
            : base(new [] { "catlist" }, "get a list of categories names")
        {
            this.db = db;
        }

        public override string CreateResponse(object[] args)
        {
            return string.Join("\n", db.SortedCategories);
        }
    }
}