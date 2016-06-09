using System.Collections.Generic;
using System.Linq;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class FollowCommand : Command
    {
        private readonly PyTaskDatabase db;
        private readonly string[] followAliases;
        private readonly Dictionary<long, List<string>> storage;

        public FollowCommand(PyTaskDatabase db, Dictionary<long, List<string>> storage, string[] followAliases)
            : base("follow", "subscribe to updates")
        {
            this.storage = storage;
            this.db = db;
            this.followAliases = followAliases;
        }

        public override bool HasAlias(string alias)
        {
            var tokens = alias.Split(' ');
            if (tokens.Length != 2)
                return false;
            return db.IsTask(GetTaskName(alias)) && followAliases.Contains(tokens[1]);
        }

        public override string CreateResponse(params object[] args)
        {
            var query = args[0] as string;
            var taskName = GetTaskName(query);
            var nullableId = args[1] as long?;
            if (nullableId == null)
                return "Произошла ошибка :3";
            var id = nullableId.Value;
            if (storage.ContainsKey(id))
                storage[id].Add(taskName);
            else
                storage[id] = new List<string> {taskName};
            return "Теперь вы следите за задачей " + taskName;
        }

        public string GetTaskName(string query)
        {
            return query.Split(' ')[0];
        }
    }
}