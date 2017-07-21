using Raptor.Data.Configuration;
using Raptor.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Configuration
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository) {
            _settingRepository = settingRepository;
        }

        /// <summary>
        /// Create a new setting
        /// </summary>
        /// <param name="setting">Setting object to be created</param>
        public void InsertSetting(Setting setting) {
            if (setting == null)
                throw new ArgumentException("Setting object cannot be null.", nameof(setting));

            _settingRepository.Create(setting);
        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting object to be updated</param>
        public void UpdateSetting(Setting setting) {
            if (setting == null)
                throw new ArgumentException("Setting object cannot be null.", nameof(setting));

            _settingRepository.Update(setting);
        }

        /// <summary>
        /// Delete a setting
        /// </summary>
        /// <param name="setting">Setting object to be deleted</param>
        public void DeleteSetting(Setting setting) {
            if (setting == null)
                throw new ArgumentException("Setting object cannot be null.", nameof(setting));

            _settingRepository.Delete(setting);
        }

        /// <summary>
        /// Delete a list of settings
        /// </summary>
        /// <param name="settings">Setting objects to be deleted</param>
        public void DeleteSettings(IList<Setting> settings) {
            foreach (var setting in settings)
                if (setting != null)
                    _settingRepository.Delete(setting);
        }

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="settingId">Id of the setting to fetch</param>
        /// <returns>Setting associated with the specified id</returns>
        public Setting GetSettingById(int settingId) {
            if (settingId == 0)
                throw new ArgumentException("Setting Id cannot be zero.", nameof(settingId));

            if (!_settingRepository.Any(settingId))
                throw new ArgumentException($"No setting found for specified id = {settingId}", nameof(settingId));

            return _settingRepository.GetById(settingId);

        }

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="key">Key of the setting</param>
        /// <returns>Setting associated with the specified key</returns>
        public Setting GetSettingByKey(string key) {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Setting key cannot be null or empty.", nameof(key));

            var setting = _settingRepository.SingleOrDefault(s => s.Name == key);

            if (setting == null)
                throw new ArgumentException($"No setting found for specified key = {key}", nameof(key));

            return setting;
        }

        /// <summary>
        /// Returns all settings in the database
        /// </summary>
        /// <returns>A list of all existing settings</returns>
        public IList<Setting> GetAllSettings() {
            return _settingRepository.GetAll().ToList();
        }

        /// <summary>
        /// Change a setting's value
        /// </summary>
        /// <param name="key">Key of the setting</param>
        /// <param name="value">New value for the setting</param>
        public void SetSetting(string key, string value) {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Setting key cannot be null or empty.", nameof(key));

            var setting = _settingRepository.SingleOrDefault(s => s.Name == key);

            if (setting == null)
                throw new ArgumentException($"No setting found for specified key = {key}", nameof(key));

            setting.Value = value;
            _settingRepository.Update(setting);
        }


        /// <summary>
        /// Checks if the setting exisits
        /// </summary>
        /// <param name="key">Key of the setting to be checked</param>
        /// <returns>True if the setting exists, otherwise false</returns>
        public bool SettingExists(string key) {
            return _settingRepository.SingleOrDefault(s => s.Name == key) != null;
        }

        /// <summary>
        /// Checks if the setting exists
        /// </summary>
        /// <param name="settingId">Id of the setting to be checked</param>
        /// <returns>True if the setting exists, otherwise false</returns>
        public bool SettingExists(int settingId) {
            return _settingRepository.Any(settingId);
        }
    }
}
