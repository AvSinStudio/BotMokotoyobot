using Telegram.Bot;

namespace PyTaskBot.App.Bot
{
    public class Sender
    {
        public void Send(Api botApi, long id, string msg)
        {
            botApi.SendTextMessage(id, msg);
        }
    }
}