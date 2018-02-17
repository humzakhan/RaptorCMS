using Microsoft.AspNetCore.Http;
using Raptor.Core.Helpers;
using Raptor.Data.Core;
using Raptor.Data.Models.Logging;
using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Logging
{
    public class CustomerActivityService : ICustomerActivityService
    {
        private readonly IRepository<ActivityLog> _customerActivityRepository;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerActivityService(IRepository<ActivityLog> customerActivityRepository, IRepository<ActivityLogType> activityLogTypeRepository, IHttpContextAccessor httpContextAccessor) {
            _customerActivityRepository = customerActivityRepository;
            _activityLogTypeRepository = activityLogTypeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Inserts an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        public void InsertActivityType(ActivityLogType activityLogType) {
            if (activityLogType == null)
                throw new ArgumentException("Null activity log type object.", nameof(activityLogType));

            _activityLogTypeRepository.Create(activityLogType);
        }

        /// <summary>
        /// Updates an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        public void UpdateActivityType(ActivityLogType activityLogType) {
            if (activityLogType == null)
                throw new ArgumentException("Null activity log type object.", nameof(activityLogType));

            _activityLogTypeRepository.Update(activityLogType);
        }

        /// <summary>
        /// Deletes an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type</param>
        public void DeleteActivityType(ActivityLogType activityLogType) {
            if (activityLogType == null)
                throw new ArgumentException("Null activity log type object.", nameof(activityLogType));

            _activityLogTypeRepository.Delete(activityLogType);
        }

        /// <summary>
        /// Gets all activity log type items
        /// </summary>
        /// <returns>Activity log type items</returns>
        public IList<ActivityLogType> GetAllActivityTypes() {
            return _activityLogTypeRepository.GetAll().ToList();
        }

        /// <summary>
        /// Gets an activity log type item
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <returns>Activity log type item</returns>
        public ActivityLogType GetActivityTypeById(int activityLogTypeId) {
            if (activityLogTypeId == 0)
                throw new ArgumentException("Activity Log Type ID cannot be zero.", nameof(activityLogTypeId));

            return _activityLogTypeRepository.GetById(activityLogTypeId);
        }

        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="user">The user who's activity is being logged</param>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        public ActivityLog InsertActivity(BusinessEntity user, string systemKeyword, string comment = "",
            params object[] commentParams) {
            if (user == null)
                return null;

            var activityType = _activityLogTypeRepository.SingleOrDefault(a => a.SystemKeyword == systemKeyword);
            if (activityType == null || !activityType.Enabled)
                return null;

            comment = CommonHelper.EnsureNotNull(comment);
            comment = string.Format(comment, commentParams);
            comment = CommonHelper.EnsureMaximumLength(comment, maxLength: 4000);

            var activityLog = new ActivityLog() {
                ActivityLogTypeId = activityType.ActivityLogTypeId,
                BusinessEntityId = user.BusinessEntityId,
                Comment = comment,
                DateCreatedUtc = DateTime.UtcNow,
                IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
            };

            _customerActivityRepository.Create(activityLog);

            return activityLog;
        }

        /// <summary>
        /// Gets an activity log item
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        public ActivityLog GetActivityById(int activityLogId) {
            if (activityLogId == 0)
                throw new ArgumentException("Activity Log Id cannot be zero.", nameof(activityLogId));

            return _customerActivityRepository.GetById(activityLogId);
        }

        /// <summary>
        /// Clears activity log
        /// </summary>
        public void ClearAllActivities() {
            var activityLogs = _customerActivityRepository.GetAll().ToList();
            _customerActivityRepository.DeleteRange(activityLogs);
        }

        /// <summary>
        /// Returns activity for the specified user
        /// </summary>
        /// <param name="userId">Id of the user for whom activity is to be returned</param>
        /// <param name="recentLogsCount">Nummber of most recent log items to return, if specified. Otherwise all log items are returned.</param>
        /// <returns>Recent ActivityLog List for the specified user</returns>
        public IEnumerable<ActivityLog> GetActivityForUser(int userId, int recentLogsCount = 0) {
            var recentActivity = recentLogsCount != 0 ?
                _customerActivityRepository
                .Find(a => a.BusinessEntityId == userId)
                .OrderByDescending(a => a.DateCreatedUtc)
                .Take(recentLogsCount) :
                _customerActivityRepository.Find(a => a.BusinessEntityId == userId);

            return recentActivity;
        }
    }
}
