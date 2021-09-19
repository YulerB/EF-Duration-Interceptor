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
        public void ScalarExecutingTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                false,
                false,
                new DateTimeOffset()
            );

            test.ScalarExecuting(testCommand, eventDefinition, new InterceptionResult<object>());
        }

        [Fact]
        public void ReaderExecutingTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                false,
                false,
                new DateTimeOffset()
            );

            test.ReaderExecuting(testCommand, eventDefinition, new InterceptionResult<DbDataReader>());
        }

        [Fact]
        public void NonQueryExecutedTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandExecutedEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                1,
                false, 
                false,
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.NonQueryExecuted(testCommand, eventDefinition, 1);
            ConnectionDoubleCompletionTest();
        }

        [Fact]
        public async Task ScalarExecutedAsyncTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandExecutedEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                null,
                false, 
                false,
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            await test.ScalarExecutedAsync(testCommand, eventDefinition, null);
        }

        [Fact]
        public void ScalarExecutedTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandExecutedEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                null,
                false, 
                false,
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.ScalarExecuted(testCommand, eventDefinition, null);
        }

//   public InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
/*
Microsoft.EntityFrameworkCore.Diagnostics.EventDefinitionBase eventDefinition, 
Func<Microsoft.EntityFrameworkCore.Diagnostics.EventDefinitionBase,Microsoft.EntityFrameworkCore.Diagnostics.EventData,string> messageGenerator, 
System.Data.Common.DbConnection connection, 
Microsoft.EntityFrameworkCore.DbContext context, 
Microsoft.EntityFrameworkCore.Diagnostics.DbCommandMethod executeMethod, 
Guid commandId, 
Guid connectionId, 
bool async, 
DateTimeOffset startTime*/
        [Fact]
        public void CommandCreatingTest()    
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandCorrelatedEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                false,
                new DateTimeOffset()
            );

            test.CommandCreating(eventDefinition, new InterceptionResult<DbCommand>());
        }

        [Fact]
        public void CommandCreatedTest()    
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandEndEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                false, 
                false,
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.CommandCreated(eventDefinition, testCommand);
        }

        [Fact]
        public void ConnectionFailedTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionErrorEventData(
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext, 
                Guid.NewGuid(), 
                new Exception("Test"),
                false, 
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.ConnectionFailed(testConnection, eventDefinition);
        }

        [Fact]
        public async Task ConnectionFailedAsyncTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionErrorEventData(
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext, 
                Guid.NewGuid(), 
                new Exception("Test"),
                false, 
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            await test.ConnectionFailedAsync(testConnection, eventDefinition, CancellationToken.None);
        }

        [Fact]
        public async Task ConnectionClosingAsyncTest()
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

            await test.ConnectionClosingAsync(testConnection, eventDefinition, new InterceptionResult());
        }

        [Fact]
        public void ConnectionClosingTest()
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

            test.ConnectionClosing(testConnection, eventDefinition, new InterceptionResult());
        }

        [Fact]
        public async Task ConnectionClosedAsyncTest()
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

            await test.ConnectionClosedAsync(testConnection, eventDefinition);
        }

        [Fact]
        public void ConnectionDoubleCompletionTest()
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
            test.ConnectionClosed(testConnection, eventDefinition);
        }

        [Fact]
        public void ConnectionCompletionWithNullContextTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            HttpContext context = null;
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (context);
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
