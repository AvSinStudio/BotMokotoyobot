using System.Collections.Generic;

namespace PyTaskBot.Infrastructure
{
    public abstract class Database<T>
    {
        private readonly string uri;

        protected HashSet<T> Data { get; private set; }

        private static HashSet<T> DownloadData(string uri) {
            var json = JsonDownloader.DownloadJson(uri);
            var db = Unmarshaller<Dictionary<string, T>>.Unmarshal(json);
            return new HashSet<T>(db.Values);
        }

        protected Database(string uri)
        {
            this.uri = uri;
            Data = DownloadData(uri);
        }

        protected Database(HashSet<T> data)
        {
            Data = data;
        }

        protected virtual void Update()
        {
            Data = DownloadData(uri);
        }
    }
}