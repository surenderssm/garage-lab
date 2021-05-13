using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class Experiment
    {
        // implicit properties
        public string Name { get; set; }
        public int Id
        {
            get;
            protected set;
        }

        // Read only propert : can be set in constructor only
        public string Code { get; }

        public Experiment(string code)
        {
            this.Code = code;
        }

        // Demonstrates why strings are immutable
        public static void OpenFile(string filename)
        {
            // what happens if caller change the file name after permission check ?

            // if (!HasPermission(filename)) throw new SecurityException();
            // return InternalOpenFile(filename);
        }


        public void ExploreTypes()
        {
            var aPoint = new { X = 100, Y = 10 };
            var bPoint = (X: 100, Y: 10);
            Console.Write(bPoint.X + aPoint.X);
        }

        private static async Task SimulatedWorkAsync()
        {

            await Task.Delay(1000);

        }

        // This method can cause a deadlock in ASP.NET or GUI context.

        public static void SyncOverAsyncDeadlock()

        {

            // Start the task.

            var delayTask = SimulatedWorkAsync();

            // Wait synchronously for the delay to complete.

            delayTask.Wait();

        }

        public async Task<string> ReadFromDb()
        {
            await Task.Delay(1);
            return "1";
        }
        
        public string ReadFromCache()
        {
            return "1";
        }

    }
}
