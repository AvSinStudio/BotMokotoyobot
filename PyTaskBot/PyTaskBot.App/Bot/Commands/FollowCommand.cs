using System.Collections.Generic;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class FollowCommand : Command
    {
        private Dictionary<long, List<string>> storage;

        public FollowCommand(PyTaskDatabase db, Dictionary<long, List<string>> storage) : base("follow", "subscribe to updates")
        {
            this.storage = storage;
        }

        public override string CreateResponse(params object[] args)
        {
            var query = args[0] as string;
            var id = args[1] as long? ?? 0;

            if (storage.ContainsKey(id))
                storage[id].Add(query);
            else
                storage[id] = new List<string> { query };

            return "Not implemented :3";
        }
    }
}