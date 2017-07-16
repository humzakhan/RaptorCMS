using Raptor.Data.Core;
using Raptor.Data.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Logging
{
    public class LogService : ILogService
    {
        private readonly IRepository<Log> _logsRepository;
        private readonly IRepository<ActivityLog> _activityLogsRepository;

        public LogService(IRepository<Log> logsRepository, IRepository<ActivityLog> activityLogsRepository)
        {
            _logsRepository = logsRepository;
            _activityLogsRepository = activityLogsRepository;
        }

        /// <summary>
        /// Inserts a new log entry in the database.
        /// </summary>
        /// <param name="logLevel">The log level for this row.</param>
        /// <param name="shortMessage">Short descriptive message for the log.</param>
        /// <param name="fullMessage">A more detail explaination of the log.</param>
        /// <returns></returns>
        public virtual Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "")
        {
            var log = new Log()
            {
                LogLevelId = (int)logLevel,
                DateCreatedUtc = DateTime.UtcNow,
                FullMessage = fullMessage,
                ShortMessage = shortMessage,
                PageUrl = string.Empty
            };

            _logsRepository.Create(log);

            return log;
        }

        /// <summary>
        /// Get a log entry by its ID.
        /// </summary>
        /// <param name="logId">ID of the log entry to fetch.</param>
        /// <returns>The log entry associated with the provided ID.</returns>
        public Log GetLogById(int logId)
        {
            return _logsRepository.GetById(logId);
        }

        /// <summary>
        /// Returns a list of log entries for the provided IDs.
        /// </summary>
        /// <param name="logIds">IDs of the log entries to fetch.</param>
        /// <returns>A list of log entries.</returns>
        public IList<Log> GetLogsByIds(int[] logIds)
        {
            return _logsRepository.Find(l => logIds.Contains(l.LogId)).ToList();
        }

        /// <summary>
        /// Delete a log entry
        /// </summary>
        /// <param name="log">The log entry to delete</param>
        public void DeleteLog(Log log)
        {
            _logsRepository.Delete(log);
        }

        /// <summary>
        /// Delete a list of log entries
        /// </summary>
        /// <param name="logs">List of log entries to elete</param>
        public void DeleteLogs(IList<Log> logs)
        {
            _logsRepository.DeleteRange(logs);
        }

        /// <summary>
        /// Delete a log entry by its ID
        /// </summary>
        /// <param name="logId">ID of the log entry to be deleted</param>
        public void DeleteLogById(int logId)
        {
            var log = _logsRepository.GetById(logId);
            _logsRepository.Delete(log);
        }

        /// <summary>
        /// Delete a list of log entries by provided their IDs in the form of a list
        /// </summary>
        /// <param name="logIds">IDs of the log entries to delete</param>
        public void DeleteLogsByIds(int[] logIds)
        {
            var logs = _logsRepository.Find(l => logIds.Contains(l.LogId)).ToList();
            _logsRepository.DeleteRange(logs);
        }
    }
}
