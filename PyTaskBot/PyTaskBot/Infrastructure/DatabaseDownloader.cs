using System.Net;
using System.Text;

namespace PyTaskBot.Infrastructure
{
    internal static class JsonDownloader
    {
        public static string DownloadJson(string url, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string json;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = encoding;
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