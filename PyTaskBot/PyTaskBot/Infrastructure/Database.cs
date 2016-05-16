using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PyTaskBot.Domain;

namespace PyTaskBot.Infrastructure
{
    public class Database
    {
        public const string Uri = @"http://pytask.info/db/tasks_full.json";

        public readonly Dictionary<string, Task> Db;

        public Database()
        {
            var json = DownloadJson(Uri);
            Db = Unmarshaller<Dictionary<string, Task>>.Unmarshal(json);
        }

        private static string DownloadJson(string url)
        {
            string json;
            using (var webClient = new WebClient())
            {
                webClient.Encoding=Encoding.UTF8;
                try
                {
                    json = webClient.DownloadString(url);
                }
                catch (WebException)
                {
                    return null;
                }
            }
            return json;
        }
    }
}