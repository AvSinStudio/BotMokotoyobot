using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PyTaskBot.Domain;

namespace PyTaskBot.Infrastructure
{
    public abstract class Database
    {


        protected readonly Dictionary<string, Task> Db;

        public Database(string uri)
        {
            var json = JsonDownloader.DownloadJson(uri);
            Db = Unmarshaller<Dictionary<string, Task>>.Unmarshal(json);
        }

       
    }
}