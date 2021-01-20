using System.Collections.Generic;

namespace ObservabilityPlatform
{
    public interface IOpReporter
    {
        bool IsEnabled { get; }
        IList<string> IncomingFilterPaths { get; }
        IList<string> OutgoingFilterPaths { get; }

        void RecordCriticalError();
        void RecordError();
        void RecordException();
        bool RecordIncomingRequest(int durationInMs, string resultCode);
        bool RecordJob(int durationInMs, string resultCode, string operationName);
        bool RecordOutgoingRequest(int durationInMs, string resultCode);
        bool RecordSosEvent(string operationName, string message);
    }
}
