namespace application_insight_dotnetcore.Models
{
    public static class TelemetryConstants
    {
        // operations
        public const string AddOfferOperation = "demoapi_offer_add";
        public const string ExpireOfferOperation = "demoapi_offer_expire";
        public const string ExpireOfferEAOperation = "demoapi_offer_expire_ea";
        public const string ExpireOfferPaidSupportOperation = "demoapi_offer_expire_paid_support";
        public const string ReactivateEaOfferOperation = "demoapi_offer_reactivate_ea";

        //
        public const string Audit = "audit";
        public const string ClientAppId = "client_app_id";
        public const string ClientAppTenantId = "client_app_tenant_id";
        public const string Succes = "200";
        public const string Fail = "500";
        public const string StatusCode = "status_code";
        public const string CustomMessage = "custom_message";
        public const string ConsistencyLevel = "consistency_level";
        public const string CosmosService = "cosmos_service";
        public const string DbService = "db_service";
        public const string Operation = "operation_code";
        public const string OperationDuration = "operation_duration";
        public const string Total = "total";

        // public const string DemoapiRequestFailedName = "demoapi_request_failed_count";

        // Events
        public const string DemoapiRequestAuthTenantFailedName = "demoapi_request_auth_tenant_failed";
        public const string CosmosServiceFailedName = "cosmos_service_failed";
        public const string DbServiceFailedName = "db_service_failed";
        public const string DemoapiOfferGetPrimaryFailedName = "demoapi_offer_get_primary_failed";
        public const string DemoapiOfferGetFailedName = "demoapi_offer_get_failed";
        public const string DemoapiOfferAddFailedName = "demoapi_offer_add_failed";
        public const string DemoapiOfferSyncFailedName = "demoapi_offer_sync_failed";
        public const string DemoapiOfferExpireFailedName = "demoapi_offer_expire_failed";
        public const string DemoapiOfferExpireEAFailedName = "demoapi_offer_expire_ea_failed";
        public const string DemoapiOfferExpirePaidSupportFailedName = "demoapi_offer_expire_paid_support_failed";
        public const string DemoapiOfferReactivateEaFailedName = "demoapi_offer_reactivate_ea_failed";
        public const string DemoapiOfferSyncInternalFailedName = "demoapi_offer_sync__internal_failed";
        public const string SyncTaskGeneratorRunFailed = "sync_taskgenerator_run_failed";
        public const string SyncTaskProcessorFailed = "sync_taskprocessor_failed";

        // Events END

        // Metrics
        public const string DbServiceCountName = "db_service_count";
        public const string DbServiceDurationName = "db_service_duration_ms";
        public const string CosmosServiceCountName = "cosmos_service_count";
        public const string CosmosServiceDurationName = "cosmos_service_duration_ms";
        public const string DemoapiOfferGetSecondaryRepoCountName = "demoapi_offer_get_secondary_repo_count";
        public const string DemoapiRequestName = "demoapi_request_count";
        public const string DemoapiRequestDurationName = "demoapi_request_duration_ms";
        public const string DemoapiOfferGetCountName = "demoapi_offer_get_count";
        public const string DemoapiOfferAddCountName = "demoapi_offer_add_count";
        public const string DemoapiOfferExpireCountName = "demoapi_offer_expire_count";
        public const string DemoapiOfferExpireEACountName = "demoapi_offer_expire_ea_count";
        public const string DemoapiOfferExpirePaidSupportCountName = "demoapi_offer_expire_paid_support_count";
        public const string DemoapiOfferReactivateEaCountName = "demoapi_offer_reactivate_ea_count";
        public const string DemoapiOfferGetDurationName = "demoapi_offer_get_duration_ms";
        public const string DemoapiOfferAddDurationName = "demoapi_offer_add_duration_ms";
        public const string DemoapiOfferExpireDurationName = "demoapi_offer_expire_duration_ms";
        public const string DemoapiOfferExpireEADurationName = "demoapi_offer_expire_ea_duration_ms";
        public const string DemoapiOfferExpirePaidSupportDurationName = "demoapi_offer_expire_paid_support_duration_ms";
        public const string DemoapiOfferReactivateEaDurationName = "demoapi_offer_reactivate_ea_duration_ms";
        public const string DemoapiOfferSyncCountName = "demoapi_offer_sync_count";
        public const string DemoapiOfferSyncDurationName = "demoapi_offer_sync_duration_ms";
        public const string SyncTaskGeneratorRunCountName = "sync_taskgenerator_run_count";
        public const string SyncTaskGeneratorRunDurationName = "sync_taskgenerator_run_duration_ms";
        public const string SyncTaskProcessorRunCountName = "sync_taskprocessor_count";
        public const string SyncTaskProcessorRunDurationName = "sync_taskprocessor_duration_ms";

    }
}