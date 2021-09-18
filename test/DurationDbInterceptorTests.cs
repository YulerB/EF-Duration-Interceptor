namespace EFDurationInterceptorTest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Moq;
    using Xunit;
    using EFDurationInterceptor;

    public class DurationDbInterceptorTests
    {
        [Fact]
        public void ConstructTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
        }

        [Fact]
        public void ConnectionClosedTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionEndEventData(
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext, 
                Guid.NewGuid(), 
                false, 
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.ConnectionClosed(testConnection, eventDefinition);
        }

        private string messageGenerator(Microsoft.EntityFrameworkCore.Diagnostics.EventDefinitionBase eventDefinitionBase,Microsoft.EntityFrameworkCore.Diagnostics.EventData eventData)
        {
            return string.Empty;
        }
    }

    internal class TestEventDefinitionBase : EventDefinitionBase {
        public TestEventDefinitionBase(ILoggingOptions loggingOptions, EventId  eventId, LogLevel logLevel, string @string)
            : base(loggingOptions, eventId, logLevel, @string)
        {
            
        }
    }
}
