namespace PyTaskBot.App.Bot.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "print help")
        {
        }

        public override string CreateResponse(string query)
        {
            return "Напиши меня.";
        }
    }
}