namespace IocContainer
{
    public interface IRepository
    {
        string ShowMessage(string message);
    }

    public class Repository : IRepository
    {
        public string ShowMessage(string message)
        {
            return message;
        }
    }
}
