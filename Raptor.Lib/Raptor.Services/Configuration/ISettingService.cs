using Raptor.Data.Models.Configuration;
using System.Collections.Generic;

namespace Raptor.Services.Configuration
{
    public interface ISettingService
    {
        /// <summary>
        /// Create a new setting
        /// </summary>
        /// <param name="setting">Setting object to be created</param>
        void InsertSetting(Setting setting);

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting object to be updated</param>
        void UpdateSetting(Setting setting);

        /// <summary>
        /// Delete a setting
        /// </summary>
        /// <param name="setting">Setting object to be deleted</param>
        void DeleteSetting(Setting setting);

        /// <summary>
        /// Delete a list of settings
        /// </summary>
        /// <param name="settings">Setting objects to be deleted</param>
        void DeleteSettings(IList<Setting> settings);

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="settingId">Id of the setting to fetch</param>
        /// <returns>Setting associated with the specified id</returns>
        Setting GetSettingById(int settingId);

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="key">Key of the setting</param>
        /// <returns>Setting associated with the specified key</returns>
        Setting GetSettingByKey(string key);

        /// <summary>
        /// Returns all settings in the database
        /// </summary>
        /// <returns>A list of all existing settings</returns>
        IList<Setting> GetAllSettings();

        /// <summary>
        /// Change a setting's value
        /// </summary>
        /// <param name="key">Key of the setting</param>
        /// <param name="value">New value for the setting</param>
        void SetSetting(string key, string value);

        /// <summary>
        /// Checks if the setting exisits
        /// </summary>
        /// <param name="key">Key of the setting to be checked</param>
        /// <returns>True if the setting exists, otherwise false</returns>
        bool SettingExists(string key);

        /// <summary>
        /// Checks if the setting exists
        /// </summary>
        /// <param name="settingId">Id of the setting to be checked</param>
        /// <returns>True if the setting exists, otherwise false</returns>
        bool SettingExists(int settingId);
    }
}
