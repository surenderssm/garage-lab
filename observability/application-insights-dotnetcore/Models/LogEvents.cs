using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace application_insight_dotnetcore.Models
{
    public class LogEvents
    {
        public const int GenerateItems = 1000;
        public const int ListItems = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;
        public const int DeleteItem = 1005;

        public const int TestItem = 3000;
        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;
        public const int TestLogEvent = 4002;
        public const int TestTestLogException = 4003;
    }
}
