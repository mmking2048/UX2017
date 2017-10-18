using UX2017.Models;

namespace UX2017
{
    public interface INewsGenerator
    {
        News GetEarningsNews();
    }

    public class NewsGenerator : INewsGenerator
    {
        public News GetEarningsNews()
        {
            return new News
            {
                Headline = "Beepers",
                Body = "New tree found"
            };
        }
    }
}