using Raptor.Data.Models.Logging;
using System.Collections.Generic;

namespace Raptor.Services.Logging
{
    public interface ILogService
    {
        /// <summary>
        /// Inserts a new log entry in the database.
        /// </summary>
        /// <param name="logLevel">The log level for this row.</param>
        /// <param name="shortMessage">Short descriptive message for the log.</param>
        /// <param name="fullMessage">A more detail explaination of the log.</param>
        /// <returns></returns>
        Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "");


        /// <summary>
        /// Get a log entry by its ID.
        /// </summary>
        /// <param name="logId">ID of the log entry to fetch.</param>
        /// <returns>The log entry associated with the provided ID.</returns>
        Log GetLogById(int logId);

        /// <summary>
        /// Returns a list of log entries for the provided IDs.
        /// </summary>
        /// <param name="logIds">IDs of the log entries to fetch.</param>
        /// <returns>A list of log entries.</returns>
        IList<Log> GetLogsByIds(int[] logIds);

        /// <summary>
        /// Delete a log entry
        /// </summary>
        /// <param name="log">The log entry to delete</param>
        void DeleteLog(Log log);

        /// <summary>
        /// Delete a list of log entries
        /// </summary>
        /// <param name="logs">List of log entries to elete</param>
        void DeleteLogs(IList<Log> logs);

        /// <summary>
        /// Delete a log entry by its ID
        /// </summary>
        /// <param name="logId">ID of the log entry to be deleted</param>
        void DeleteLogById(int logId);

        /// <summary>
        /// Delete a list of log entries by provided their IDs in the form of a list
        /// </summary>
        /// <param name="logIds">IDs of the log entries to delete</param>
        void DeleteLogsByIds(int[] logIds);
    }
}
