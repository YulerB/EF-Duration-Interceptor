# EF-Duration-Interceptor

[![.NET](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/dotnet.yml/badge.svg)](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/dotnet.yml)
[![.NET GITHUB PACKAGE](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/dotnet.github.package.yml/badge.svg)](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/dotnet.github.package.yml)
[![CodeQL](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/codeql-analysis.yml)
![NuGet Badge](https://buildstats.info/nuget/EFDurationInterceptor)
[![Sonarqube](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/sonarqube.yml/badge.svg)](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/sonarqube.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=YulerB_EF-Duration-Interceptor&metric=alert_status)](https://sonarcloud.io/dashboard?id=YulerB_EF-Duration-Interceptor)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=YulerB_EF-Duration-Interceptor&metric=coverage)](https://sonarcloud.io/dashboard?id=YulerB_EF-Duration-Interceptor)

Entity Framework Command Duration Interceptor - Outputs as a response header.


To add the interceptor to DbContext call the AddInterceptors method when configuring your DbContext.

```csharp

using EFDurationInterceptor;
...
services
  .AddDbContext<NvmContext>((provider, options) => options
  .UseSqlServer(connectionString)
  .AddInterceptors(provider.GetRequiredService<DurationDbInterceptor>())); 
```

A value is added to the response headers for both the duration setting up connections and the duration executing commands. 

![Screenshot](https://raw.githubusercontent.com/YulerB/EF-Duration-Interceptor/main/output.jpg)
