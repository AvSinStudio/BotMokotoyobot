using System;
using System.Collections.Generic;
using System.Linq;
using PyTaskBot.Domain;

namespace PyTaskBot.Infrastructure
{
    public class PyTaskDatabase : Database<Task>
    {
        public List<string> SortedTasksNames;
        public HashSet<string> TasksNamesSet;

        public List<string> SortedCategories;
        public HashSet<string> CategoriesSet;

        public PyTaskDatabase(string uri) : base(uri)
        {
            Init();
        }

        private PyTaskDatabase(HashSet<Task> data) : base(data)
        {
            Init();
        }
        
        private void Init()
        {
            TasksNamesSet = GetAll(x => x.Name);
            SortedTasksNames = TasksNamesSet.OrderBy(x => x).ToList();

            CategoriesSet = GetAll(x => x.Category);
            SortedCategories = CategoriesSet.OrderBy(x => x).ToList();
        }

        private HashSet<string> GetAll(Func<Task, string> fieldGetter)
        {
            var elements = Data.Select(fieldGetter);
            return new HashSet<string>(elements, StringComparer.OrdinalIgnoreCase);
        }


        public Task GetTask(string name)
        {
            return Data.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task GetTaskWithMax<T>(Func<Task, T> scopeFunc)
        {
            return Data.OrderByDescending(scopeFunc).FirstOrDefault();
        }

        public Task GetTaskWithMin<T>(Func<Task, T> scopeFunc)
        {
            return Data.OrderBy(scopeFunc).FirstOrDefault();
        }

        public bool IsTask(string query) {
            return TasksNamesSet.Contains(query);
        }

        public bool IsCategory(string query) {
            return CategoriesSet.Contains(query, StringComparer.OrdinalIgnoreCase);
        }

        private IEnumerable<Task> FilterByCategory(string category)
        {
            return Data.Where(x => x.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<string> GetNamesOfTasksInCategory(string category)
        {
            return FilterByCategory(category).Select(x => x.Name);
        }

        public PyTaskDatabase GetTaskInCategory(string category)
        {
            var data = new HashSet<Task>(FilterByCategory(category));
            return new PyTaskDatabase(data);
        }
    }
}