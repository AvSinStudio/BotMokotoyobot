namespace PyTaskBot.App.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base(new [] { "help" }, "print help")
        {
        }

        public override string CreateResponse(object[] args)
        {
            return "Справка";
        }
    }
}