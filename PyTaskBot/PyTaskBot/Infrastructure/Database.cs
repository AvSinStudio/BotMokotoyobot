using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PyTaskBot.Domain;

namespace PyTaskBot.Infrastructure
{
    public abstract class Database<T>
    {


        protected readonly Dictionary<string, T> Db;

        protected Database(string uri)
        {
            var json = JsonDownloader.DownloadJson(uri);
            Db = Unmarshaller<Dictionary<string, T>>.Unmarshal(json);
        }

        protected Database(Dictionary<string, T> db)
        {
            this.Db = db;
        }


    }
}