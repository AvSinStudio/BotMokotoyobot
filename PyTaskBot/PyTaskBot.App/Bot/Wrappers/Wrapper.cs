using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Wrappers
{
    public abstract class Wrapper
    {
        protected PyTaskDatabase db;
        public Wrapper(PyTaskDatabase db)
        {
            this.db = db;
        }
        public abstract string GetWrapped(string query);
    }
}