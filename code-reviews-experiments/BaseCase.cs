using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class BaseCase
    {
        public async Task DoWork()
        {
            await Task.Delay(1);
        }

        public async Task<int> DoWorkWithNumber(int x)
        {
            await Task.Delay(1);
            return x;
        }
    }
}
