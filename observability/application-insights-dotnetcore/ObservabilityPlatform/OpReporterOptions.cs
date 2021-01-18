using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.Extensions.Configuration;

namespace application_insight_dotnetcore.ObservabilityPlatform
{
    public class OpReporterOptions
    {
        public bool IsEnabled { get; set; }
        public string InstrumentationKey { get; set; }
        public string ServiceLine { get; set; }
        public string ServiceName { get; set; }
        public IList<string> IncomingFilterPaths { get; set; }
        public IList<string> OutgoingFilterPaths { get; set; }

        /// <summary>
        /// Pre-processing on the members for consistency of the platform
        /// </summary>
        public void PreProcess()
        {
            // To avoid case realted query/alert Issues on the platform
            if (string.IsNullOrWhiteSpace(ServiceLine) == false)
            {
                ServiceLine = ServiceLine.ToLower();
            }

            if (string.IsNullOrWhiteSpace(ServiceName) == false)
            {
                ServiceName = ServiceName.ToLower();
            }
        }
    }
}
