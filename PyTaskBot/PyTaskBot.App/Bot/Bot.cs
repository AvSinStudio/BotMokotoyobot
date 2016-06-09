using System.Collections.Generic;

namespace PyTaskBot.App.Bot
{
    public abstract class Bot<TUpdate>
    {
        private Executor Executor { get; set; }
        public abstract void SendMessage(TUpdate update, string response);
        public abstract IEnumerable<TUpdate> GetUpdates();
        public abstract void ListenAndAnswer();
    }
}