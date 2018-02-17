using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Data.Models.Configuration;
using Raptor.Data.Models.Logging;
using Raptor.Services.Configuration;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Web.Areas.Admin.ViewModels;
using System;
using System.Linq;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    public class SettingsController : Controller
    {
        private readonly ILogService _logService;
        private readonly ISettingService _settingsService;
        private readonly ICustomerActivityService _activityService;
        private readonly IWorkContext _workContext;

        public SettingsController(ISettingService settingsService, ILogService logService, ICustomerActivityService activityService, IWorkContext workContext) {
            _settingsService = settingsService;
            _logService = logService;
            _activityService = activityService;
            _workContext = workContext;
        }

        public IActionResult Index() {
            return RedirectToAction("General");
        }

        [HttpGet]
        public IActionResult General() {
            var settings = _settingsService.GetAllSettings();
            var model = new SettingsViewModel() {
                SiteName = settings.First(s => s.Name == SettingsConstants.SiteName).Value,
                SiteDescription = settings.First(s => s.Name == SettingsConstants.SiteDescription).Value,
                IsRegistrationEnabled = Convert.ToBoolean(settings.First(s => s.Name == SettingsConstants.SiteRegistrationEnabled).Value),
                IsCommentsEnabled = Convert.ToBoolean(settings.First(s => s.Name == SettingsConstants.SiteCommmentsEnabled).Value),
                FacebookUrl = settings.First(s => s.Name == SettingsConstants.FacebookUrl).Value,
                TwitterUrl = settings.First(s => s.Name == SettingsConstants.TwitterUrl).Value,
                InstagramUrl = settings.First(s => s.Name == SettingsConstants.InstagramUrl).Value,
                YoutubeUrl = settings.First(s => s.Name == SettingsConstants.YoutubeUrl).Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult General(SettingsViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                _settingsService.SetSetting(SettingsConstants.SiteName, model.SiteName);
                _settingsService.SetSetting(SettingsConstants.SiteDescription, model.SiteDescription);
                _settingsService.SetSetting(SettingsConstants.SiteRegistrationEnabled, model.IsRegistrationEnabled.ToString());
                _settingsService.SetSetting(SettingsConstants.SiteCommmentsEnabled, model.IsCommentsEnabled.ToString());
                _settingsService.SetSetting(SettingsConstants.FacebookUrl, model.FacebookUrl);
                _settingsService.SetSetting(SettingsConstants.TwitterUrl, model.TwitterUrl);
                _settingsService.SetSetting(SettingsConstants.InstagramUrl, model.InstagramUrl);
                _settingsService.SetSetting(SettingsConstants.YoutubeUrl, model.YoutubeUrl);

                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.UpdateSettings, "Updated the settings");

                ViewBag.Status = "OK";
                ViewBag.Message = "Your changes have been saved.";
            }
            catch (Exception ex) {
                _logService.InsertLog(LogLevel.Error, $"Unable to update site settings: {ex.Message}", ex.ToString());
                ModelState.AddModelError("", $"Unable to update site settings: {ex}");
            }

            return View(model);
        }
    }
}