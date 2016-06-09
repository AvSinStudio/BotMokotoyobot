using System.Collections.Generic;
using System.Linq;
using PyTaskBot.Infrastructure;

namespace PyTaskBot.App.Commands
{
    public class FollowCommand : Command
    {
        private static readonly string[] names = { "фоллоу", "follow", "следить" };

        private readonly PyTaskDatabase db;
        private readonly Dictionary<long, List<string>> storage;

        public FollowCommand(PyTaskDatabase db, Dictionary<long, List<string>> storage)
            : base(names, "subscribe to updates")
        {
            this.storage = storage;
            this.db = db;
        }

        public override bool CanBeCalledBy(string name)
        {
            var tokens = name.Split(' ');
            if (tokens.Length != 2)
                return false;
            return db.IsTask(GetTaskName(name)) && names.Contains(tokens[1]);
        }

        public override string CreateResponse(object[] args)
        {
            var query = args[0] as string;
            var taskName = GetTaskName(query);

            var nullableId = args[1] as long?;
            if (!nullableId.HasValue) return "Произошла ошибка";
            var id = nullableId.Value;

            if (storage.ContainsKey(id))
            {
                storage[id].Add(taskName);
            }
            else
            {
                storage[id] = new List<string> { taskName };
            }

            return "Теперь вы следите за задачей " + taskName;
        }

        private static string GetTaskName(string query)
        {
            return query.Split(' ')[0];
        }
    }
}