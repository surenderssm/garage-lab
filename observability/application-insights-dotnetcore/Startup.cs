using application_insight_dotnetcore.ObservabilityPlatform;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace application_insight_dotnetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // register opTelemetryProcessor and related items
            services.AddSingleton<IOpReporter, OpReporter>();
            // TODO : check out with Injection not working in Telemetry processor
            // might be a bug on the lines of https://github.com/microsoft/ApplicationInsights-dotnet/issues/1536

            var intermediateServiceProvider = services.BuildServiceProvider();
            var reporter = intermediateServiceProvider.GetService<IOpReporter>();
            OpReporterProvider.Reporter = reporter;

            // Register application Insights
            services.AddApplicationInsightsLoggerProvider(Configuration);
            services.AddApplicationInsightsTelemetryProcessor<OpMetricTelemetryProcessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
