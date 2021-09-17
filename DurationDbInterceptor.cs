namespace EFDurationInterceptor
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
 
    public class DurationDbInterceptor : IDbCommandInterceptor, IDbConnectionInterceptor
    {
        public const string XDbCommandMsHeader = "X-DB-COM-MS";
        public const string XDbConnectionMsHeader = "X-DB-CON-MS";

        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly List<DbContextEventData> events;

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationDbInterceptor"/> class.
        /// Creates a formatter that will log every command from any context and also commands that do not originate from a context.
        /// </summary>
        /// <param name="httpContextAccessor">The delegate to which output will be sent.</param>
        public DurationDbInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.events = new List<DbContextEventData>();
        }

        public InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            events.Add(eventData);
            return result;
        }

        public DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            events.Add(eventData);
            return result;
        }

        public InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            events.Add(eventData);
            return result;
        }

        public InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            events.Add(eventData);
            return result;
        }

        public InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            events.Add(eventData);
            return result;
        }

        public Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public Task<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public Task<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            events.Add(eventData);
            return result;
        }

        public object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            events.Add(eventData);
            return result;
        }

        public int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            events.Add(eventData);
            return result;
        }

        public Task<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public Task<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public Task<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            events.Add(eventData);
        }

        public Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.CompletedTask;
        }

        public InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
        {
            events.Add(eventData);
            return result;
        }

        public InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            events.Add(eventData);
            return result;
        }

        public Task<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            events.Add(eventData);
        }

        public Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            return Task.CompletedTask;
        }

        public InterceptionResult ConnectionClosing(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            events.Add(eventData);
            return result;
        }

        public Task<InterceptionResult> ConnectionClosingAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            events.Add(eventData);
            return Task.FromResult(result);
        }

        public void ConnectionClosed(DbConnection connection, ConnectionEndEventData eventData)
        {
            events.Add(eventData);
            OnComplete(events);
            events.Clear();
        }

        public Task ConnectionClosedAsync(DbConnection connection, ConnectionEndEventData eventData)
        {
            events.Add(eventData);
            OnComplete(events);
            events.Clear();
            return Task.CompletedTask;
        }

        public void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
        {
            events.Add(eventData);
            OnComplete(events);
            events.Clear();
        }

        public Task ConnectionFailedAsync(DbConnection connection, ConnectionErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            events.Add(eventData);
            OnComplete(events);
            events.Clear();
            return Task.CompletedTask;
        }

        protected void OnComplete(List<DbContextEventData> eventDataList)
        {
            var commandDuration = (
                from item in eventDataList
                where item is CommandExecutedEventData
                let evData = item as CommandExecutedEventData
                select evData.Duration.TotalMilliseconds).Sum();

            var connectionDuration = (
                from item in eventDataList
                where item is ConnectionEndEventData
                let evData = item as ConnectionEndEventData
                select evData.Duration.TotalMilliseconds).Sum();

            var headers = httpContextAccessor.HttpContext.Response.Headers;

            if (headers.ContainsKey(XDbCommandMsHeader))
            {
                var updatedCommandDuration = double.Parse(headers[XDbCommandMsHeader][0]) + commandDuration;
                headers[XDbCommandMsHeader] = new StringValues(updatedCommandDuration.ToString());
            }
            else
            {
                headers.Add(XDbCommandMsHeader, new StringValues(commandDuration.ToString()));
            }

            if (headers.ContainsKey(XDbConnectionMsHeader))
            {
                var updatedConnectionDuration = double.Parse(headers[XDbConnectionMsHeader][0]) + connectionDuration;
                headers[XDbConnectionMsHeader] = new StringValues(updatedConnectionDuration.ToString());
            }
            else
            {
                headers.Add(XDbConnectionMsHeader, new StringValues(connectionDuration.ToString()));
            }
        }
    }
}
