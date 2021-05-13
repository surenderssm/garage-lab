using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class CaseFour : BaseCase
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

        private static async Task SimulatedWorkAsync()
        {

            await Task.Delay(1000);

        }

        // This method can cause a deadlock in ASP.NET or GUI context.
        public void SyncOverAsyncDeadlock()
        {

            // Start the task.
            var delayTask = SimulatedWorkAsync();

            // Wait synchronously for the delay to complete.
            delayTask.Wait();
        }


        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public Task SleepAsync(int millisecondsTimeout)
        {
            return Task.Run(() => Sleep(millisecondsTimeout));
        }


        public async Task<int> ComputeUsageAsync()
        {

            try

            {

                var operand = await DoWorkWithNumber(10);

                var operand2 = await DoWorkWithNumber(23);

                return operand + operand2;
            }

            catch (KeyNotFoundException)
            {
                return 0;
            }
        }

        public int ComputeUsage()
        {

            try

            {

                var operand = DoWorkWithNumber(10).Result;

                var operand2 = DoWorkWithNumber(23).Result;

                return operand + operand2;

            }

            catch (AggregateException e)

            when (e.InnerExceptions.FirstOrDefault().GetType()

                == typeof(KeyNotFoundException))
            {

                return 0;

            }
        }
    }
}
