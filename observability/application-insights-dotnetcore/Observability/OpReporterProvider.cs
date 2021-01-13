namespace application_insight_dotnetcore
{
    public static class OpReporterProvider
    {
        /// <summary>
        /// Instance to manage life of Reporter
        /// </summary>
        /// <value></value>
        public static IOpReporter Reporter { get; set; }
    }
}
