namespace DataBase.Infrastructure
{
    public class Database
    {
        
        public string DownloadJson(string url)
        {
            var json = "";
            using (var webClient = new System.Net.WebClient())
                json = webClient.DownloadString(url);
            return json;
        }
    }
}