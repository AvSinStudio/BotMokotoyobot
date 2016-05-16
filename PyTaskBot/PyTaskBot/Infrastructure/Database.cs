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

        public Database()
        {
            var json = DownloadJson(Uri);
            var db = Unmarshaller<Dictionary<string, Task>>.Unmarshal(json);
            Console.ReadKey();
        }

        public string DownloadJson(string url = Uri)
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