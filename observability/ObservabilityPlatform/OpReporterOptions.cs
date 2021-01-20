using System;
using System.Collections.Generic;

namespace ObservabilityPlatform
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
        /// ValidateAndPreProcess Pre-processing on the members for consistency of the platform
        /// </summary>
        public void ValidateAndPreProcess()
        {

            if (string.IsNullOrWhiteSpace(ServiceLine))
            {
                throw new ArgumentNullException(nameof(ServiceLine));
            }

            if (string.IsNullOrWhiteSpace(ServiceName))
            {
                throw new ArgumentNullException(nameof(ServiceName));
            }

            if (string.IsNullOrWhiteSpace(InstrumentationKey))
            {
                throw new ArgumentNullException(nameof(InstrumentationKey));
            }

            ServiceLine = ServiceLine.ToLower();
            ServiceName = ServiceName.ToLower();
        }
    }
}
