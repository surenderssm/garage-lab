Steps to start something like this from scratch.

### 1. Install package

Add a reference to the
[`OpenTelemetry.Instrumentation.AspNetCore`](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNetCore)
package.

Add pacakge without --version might not work as of 3 January 2021, as these are only prerelease version is available.

```shell
dotnet add package OpenTelemetry.Instrumentation.AspNetCore --version 1.0.0-rc1.1
dotnet add package OpenTelemetry.Instrumentation.Http --version 1.0.0-rc1.1
```

```shell
dotnet add package OpenTelemetry.Extensions.Hosting --version 1.0.0-rc1.1
dotnet add package OpenTelemetry.Exporter.Console --version 1.0.0-rc1.1
dotnet add package System.Diagnostics.DiagnosticSource
```
References

https://github.com/open-telemetry/opentelemetry-dotnet/blob/master/src/OpenTelemetry.Instrumentation.AspNetCore/README.md
