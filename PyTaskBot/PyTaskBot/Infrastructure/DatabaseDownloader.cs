using System.Net;
using System.Text;

namespace PyTaskBot.Infrastructure
{
    internal static class JsonDownloader
    {
        public static string DownloadJson(string url)
        {
            string json;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
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