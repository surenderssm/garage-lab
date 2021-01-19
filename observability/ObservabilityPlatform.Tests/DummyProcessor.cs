using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Channel;

namespace ObservabilityPlatform.Tests
{
    public class DummyProcessor : ITelemetryProcessor
    {
        public int Count { get; set; }

        public DummyProcessor()
        {
            Count = 0;
        }
        public void Process(ITelemetry item)
        {
            Count++;
            Console.Write("Processed");
        }
    }
}
