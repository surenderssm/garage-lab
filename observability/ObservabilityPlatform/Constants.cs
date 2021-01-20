namespace ObservabilityPlatform
{
    /// <summary>
    /// Constants for the platform
    /// </summary>
    public class Constants
    {
        public const string SosEventName = "op_sos";
        public const string IncomingRequestsDurationMetricName = "op_incoming_requests_duration_ms";
        public const string OutgoingRequestsDurationMetricName = "op_outgoing_requests_duration_ms";
        public const string ErrorsCountMetricName = "op_errors_count";
        public const string JobDurationMetricName = "op_job_duration_ms";
        public const string ServiceLineKey = "service_line";
        public const string ServiceNameKey = "service_name";
        public const string ResultCodeKey = "result_code";
        public const string ErrorTypeKey = "error_type";
        public const string OperationNameKey = "operation_name";
        public const string MessageKey = "message";
        public const string ErrorKey = "error";
        public const string CriticalKey = "critical";
        public const string ExceptionKey = "exception";
        public const string ObservabilityPlatformConfigKey = "ObservabilityPlatform";
    }
}