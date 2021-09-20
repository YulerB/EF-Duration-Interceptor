namespace EFDurationInterceptorTest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using System.Data;
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
        public async Task CommandFailedAsyncTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandErrorEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                new Exception("test"),
                false,
                false,
                new DateTimeOffset(),
                TimeSpan.FromSeconds(1)
            );

            await test.CommandFailedAsync(testCommand, eventDefinition, CancellationToken.None);
        }

        [Fact]
        public void CommandFailedTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            SqlCommand testCommand = testConnection.CreateCommand();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new CommandErrorEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                testCommand,
                null,//DbContext,
                DbCommandMethod.ExecuteReader,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                new Exception("test"),
                false,
                false,
                new DateTimeOffset(),
                TimeSpan.FromSeconds(1)
            );

            test.CommandFailed(testCommand, eventDefinition);
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
        public async Task ReaderExecutingAsyncTest()
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

            await test.ReaderExecutingAsync(testCommand, eventDefinition, new InterceptionResult<DbDataReader>());
        }

        [Fact]
        public async Task ScalarExecutingAsyncTest()
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

            await test.ScalarExecutingAsync(testCommand, eventDefinition, new InterceptionResult<object>());
        }


        [Fact]
        public async Task NonQueryExecutedAsyncTest()
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

            await test.NonQueryExecutedAsync(testCommand, eventDefinition, 1);
        }

        [Fact]
        public void NonQueryExecutingTest()
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

            test.NonQueryExecuting(testCommand, eventDefinition, new InterceptionResult<int>());
        }

        [Fact]
        public async Task NonQueryExecutingAsyncTest()
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

            await test.NonQueryExecutingAsync(testCommand, eventDefinition, new InterceptionResult<int>());
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
        public void ReaderExecutedTest()
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
                new SingleResultReader(new List<object>()),
                false, 
                false,
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.ReaderExecuted(testCommand, eventDefinition, new SingleResultReader(new List<object>()));
            ConnectionDoubleCompletionTest();
        }

        [Fact]
        public async Task ReaderExecutedAsyncTest()
        {
            var context = new DefaultHttpContext();
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (context);
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
                new SingleResultReader(new List<object>()),
                false, 
                false,
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            await test.ReaderExecutedAsync(testCommand, eventDefinition, new SingleResultReader(new List<object>()));
 
           var eventDefinition1 = new ConnectionEndEventData(
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext, 
                Guid.NewGuid(), 
                false, 
                new DateTimeOffset(), 
                TimeSpan.FromSeconds(1)
            );

            test.ConnectionClosed(testConnection, eventDefinition1);
           Assert.True(context.Response.Headers.ContainsKey(DurationDbInterceptor.XDbCommandMsHeader));
         }    

        [Fact]
        public void ConnectionOpeningTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext,
                Guid.NewGuid(), 
                false,
                new DateTimeOffset()
            );

            test.ConnectionOpening(testConnection, eventDefinition, new InterceptionResult());
        }    

        [Fact]
        public void ConnectionOpenedTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionEndEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext,
                Guid.NewGuid(), 
                false,
                new DateTimeOffset(),
                TimeSpan.FromSeconds(1)
            );

            test.ConnectionOpened(testConnection, eventDefinition);
        }    

        [Fact]
        public async Task ConnectionOpenedAsyncTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionEndEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext,
                Guid.NewGuid(), 
                false,
                new DateTimeOffset(),
                TimeSpan.FromSeconds(1)
            );

            await test.ConnectionOpenedAsync(testConnection, eventDefinition, CancellationToken.None);
        }    

        [Fact]
        public async Task ConnectionOpeningAsyncTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns (new DefaultHttpContext());
            Mock<ILoggingOptions> loggingOptionsMock = new Mock<ILoggingOptions>();
            DurationDbInterceptor test = new DurationDbInterceptor(httpContextAccessorMock.Object);
            SqlConnection testConnection = new SqlConnection();
            var testDefinition = new TestEventDefinitionBase(loggingOptionsMock.Object, new EventId(1),LogLevel.Information, "test");
            var eventDefinition = new ConnectionEventData (
                testDefinition,  
                messageGenerator, 
                testConnection, 
                null,//DbContext,
                Guid.NewGuid(), 
                false,
                new DateTimeOffset()
            );

            await test.ConnectionOpeningAsync(testConnection, eventDefinition, new InterceptionResult(), CancellationToken.None);
        }    

        [Fact]
        public async Task ScalarExecutedAsyncTest()
        {
            var context = new DefaultHttpContext();
            Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();   
            httpContextAccessorMock.Setup(_ => _.HttpContext).Returns(context);
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

    internal class SingleResultReader
        : DbDataReader  
    {
    
        protected IEnumerable<object> Items { get; private set; }
    
        protected IEnumerator<object> Enumerator { get; private set; }
    
        public SingleResultReader(
            IEnumerable<object> items)
        {
            Items = items;
            Enumerator = Items.GetEnumerator();
        }
    
        public override bool IsClosed
        {
            get { return Enumerator == null; }
        }
    
        public override int RecordsAffected
        {
            get { return Items.Count(); }
        }
    
        public override int FieldCount
        {
            get { return 1; }
        }
    
        public override bool HasRows
        {
            get { return Items != null && Items.Count() > 0; }
        }
    
        public override void Close()
        {
            Enumerator = null;
            Items = null;
        }
        
        public override bool Read()
        {
            return Enumerator.MoveNext();
        }
    
        public override IEnumerator<object> GetEnumerator()
        {
            return Enumerator;
        }
    
        public override int GetOrdinal(string name)
        {
            return 1;
        }
    
        public override object this[int ordinal]
        {
            get { throw new NotSupportedException(); }
        }
    
        public override object this[string name]
        {
            get { throw new NotSupportedException(); }
        }
    
        public override int Depth
        {
            get { throw new NotSupportedException(); }
        }
    
        public override string GetName(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }
    
        public override bool NextResult()
        {
            throw new NotSupportedException();
        }
    
        public override bool GetBoolean(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override byte GetByte(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotSupportedException();
        }
    
        public override char GetChar(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override long GetChars(
            int ordinal,
            long dataOffset,
            char[] buffer,
            int bufferOffset,
            int length)
        {
            throw new NotSupportedException();
        }
    
        public override Guid GetGuid(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override short GetInt16(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override int GetInt32(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override long GetInt64(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override DateTime GetDateTime(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override string GetString(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override object GetValue(int fieldIndex)
        {
            throw new NotSupportedException();
        }

        public override int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }
    
        public override bool IsDBNull(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override decimal GetDecimal(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override double GetDouble(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override float GetFloat(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override string GetDataTypeName(int ordinal)
        {
            throw new NotSupportedException();
        }
    
        public override Type GetFieldType(int ordinal)
        {
            throw new NotSupportedException();
        }
    }
    
 
}
