# EF-Duration-Interceptor

![example workflow](https://github.com/YulerB/EF-Duration-Interceptor/actions/workflows/.NET/badge.svg)

Entity Framework Command Duration Interceptor - Outputs as a response header.


To add the interceptor to DbContext call the AddInterceptors method when configuring your DbContext.

`
using EFDurationInterceptor;
...
services
  .AddDbContext<NvmContext>((provider, options) => options
  .UseSqlServer(connectionString)
  .AddInterceptors(provider.GetRequiredService<DurationDbInterceptor>())); 
`
