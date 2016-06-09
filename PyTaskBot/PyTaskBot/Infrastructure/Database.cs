using System.Collections.Generic;

namespace PyTaskBot.Infrastructure
{
    public abstract class Database<T>
    {
        private readonly string uri;

        protected Database(string uri)
        {
            this.uri = uri;
            Data = GetData(uri);
        }

        protected Database(HashSet<T> data)
        {
            Data = data;
        }

        protected HashSet<T> Data { get; set; }

        private HashSet<T> GetData(string uri)
        {
            var json = JsonDownloader.DownloadJson(uri);
            return new HashSet<T>(Unmarshaller<Dictionary<string, T>>.Unmarshal(json).Values);
        }

        public virtual void Update()
        {
            Data = GetData(uri);
        }
    }
}