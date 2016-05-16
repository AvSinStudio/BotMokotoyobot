using System.Net;

namespace PyTaskBot.Infrastructure
{
    public class Database
    {
        public const string Uri = @"http://pytask.info/db/tasks_full.json";

        public string DownloadJson(string url = Uri)
        {
            string json;
            using (var webClient = new WebClient())
            {
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