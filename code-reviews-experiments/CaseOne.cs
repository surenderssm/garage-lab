using System;
using System.Security;
using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class FileRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }

    public class CaseOne : BaseCase
    {

        public async Task ExecuteAsync()
        {
            await DoWork();
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
