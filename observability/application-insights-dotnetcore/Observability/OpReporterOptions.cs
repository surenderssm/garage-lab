using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.Extensions.Configuration;

namespace application_insight_dotnetcore
{
    public class OpReporterOptions
    {
        public bool IsEnabled { get; set; }
        public string InstrumentationKey { get; set; }
        public string ServiceLine { get; set; }
        public string ServiceName { get; set; }
        public IList<string> IncomingFilterPaths { get; set; }
        public IList<string> OutgoingFilterPaths { get; set; }
    }
}
