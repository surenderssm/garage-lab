using System.Security;
using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class FileRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }
// Immutability : Whose state cannot be modified after it is created.

// String objects are immutable: they cannot be changed after they have been created. All of the String methods and C# operators that appear to modify a string actually return the results in a new string object


    public class CaseOne : BaseCase
    {
        public async Task ExecuteAsync()
        {
            var request = new FileRequest { Name = "1", Author = "v8" };
            await OpenFile(request);
        }

        public async Task OpenFile(FileRequest request)
        {
            if (!HasPermission(request.Name)) throw new SecurityException();
            await DoWork();
            await InteralProcess(request.Name);
        }

        private bool HasPermission(string name)
        {
            // some logic
            return true;
        }

        private async Task InteralProcess(string name)
        {
            // some logic
            await DoWork();
        }

        public async Task OpenFile(string filename)
        {
            if (!HasPermission(filename)) throw new SecurityException();
            await DoWork();
            await InteralProcess(filename);
        }
    }
}
