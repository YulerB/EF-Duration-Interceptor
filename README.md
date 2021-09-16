# EF-Duration-Interceptor

![example workflow](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/dotnet.yml/badge.svg)
[![NuGet Badge](https://buildstats.info/nuget/EFDurationInterceptor )](https://www.nuget.org/packages/EFDurationInterceptor /)

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
