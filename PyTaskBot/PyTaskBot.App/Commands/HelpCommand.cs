namespace PyTaskBot.App.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "print help")
        {
        }

        public override string CreateResponse(params object[] args)
        {
            return "Напиши меня.";
        }
    }
}