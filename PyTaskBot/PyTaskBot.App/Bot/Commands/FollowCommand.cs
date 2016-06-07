using System;
using System.Collections.Generic;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Bot.Commands
{
    public class FollowCommand : Command
    {
        private Dictionary<string, List<string>> storage;
        public FollowCommand(PyTaskDatabase db, Dictionary<string, List<string>> storage) : base("follow", "subscribe to updates")
        {
            this.storage = storage;
        }

        public override string CreateResponse(string[] args)
        {
            var query = args[0];
            var id = args[1];
            if (storage.ContainsKey(id))
                storage[id].Add(query);
            else
                storage[id] = new List<string>(){ query };
            return "Not implemented :3";
        }
    }
}