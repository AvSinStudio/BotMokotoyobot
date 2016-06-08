using System.Collections.Generic;

namespace PyTaskBot.Infrastructure
{
    public abstract class Database<T>
    {
        protected readonly HashSet<T> Data;
        
        protected Database(string uri)
        {
            var json = JsonDownloader.DownloadJson(uri);
            Data = new HashSet<T>(Unmarshaller<Dictionary<string, T>>.Unmarshal(json).Values);
        }

        protected Database(HashSet<T> data)
        {
            Data = data;
        }
    }
}