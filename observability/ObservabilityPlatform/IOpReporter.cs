using Microsoft.ApplicationInsights.Channel;

namespace ObservabilityPlatform
{
    public interface IOpReporter
    {
        void Process(ITelemetry item);
        void RecordCriticalError();
        void RecordError();
        void RecordException();
        void RecordIncomingRequest(int durationInMs, string resultCode);
        void RecordJob(int durationInMs, string resultCode, string operationName);
        void RecordOutgoingRequest(int durationInMs, string resultCode);
        
        // RecordSosEvent app can use to raise an SOS to seek immediate attention. Like queue full, cert expired, invalid keys,app not able to startup
        void RecordSosEvent(string operationName, string message);
    }
}
