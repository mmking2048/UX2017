namespace UX2017
{
    public interface INewsGenerator
    {
        string GetEarningsNews();
    }

    public class NewsGenerator : INewsGenerator
    {
        public string GetEarningsNews()
        {
            return "Beep";
        }
    }
}