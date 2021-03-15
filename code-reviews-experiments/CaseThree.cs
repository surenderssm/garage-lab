using System;
using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class CaseThree : BaseCase
    {
        public async Task ExecuteAsync()
        {
            Example();
            await DoWork();
        }

        public void Example()
        {
            var pointA = new { X = 5, Y = 67 };

            var pointB = (X: 100, Y: 10);

            Console.Write(pointB.X);
            Console.Write(pointA.X);

        }
    }
}
