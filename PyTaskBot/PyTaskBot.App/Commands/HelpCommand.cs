namespace PyTaskBot.App.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "print help")
        {
        }

        public override string CreateResponse(params object[] args)
        {
            return $"Возможные команды: \n" +
                   $"/list - выдаст список всех задач \n" +
                   $"/catlist - выдаст список всех категорий задач \n\n" +
                   $"Возможные запросы: \n" +
                   $"%Название задачи% - информация по данной задаче \n" +
                   $"%Название категории% - задачи в данной категории \n" +
                   $"Максимальный/минимальный средний процент/балл в категории %название категории% \n" +
                   $"Максимальное/минимальное количество сдавших в категории %название категории% \n";
        }
    }
}