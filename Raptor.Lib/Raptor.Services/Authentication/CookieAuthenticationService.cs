using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Raptor.Core.Helpers;
using Raptor.Data.Models.Logging;
using Raptor.Data.Models.Users;
using Raptor.Services.Logging;
using Raptor.Services.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Raptor.Services.Authentication
{
    public class CookieAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserService _userAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerActivityService _activityTracker;
        private readonly ILogService _logFactory;
        private Person _cachedUser;

        public CookieAuthenticationService(IUserService userAccountService, IHttpContextAccessor httpContextAccessor, ICustomerActivityService activityTracker, ILogService logFactory) {
            _userAccountService = userAccountService;
            _httpContextAccessor = httpContextAccessor;
            _activityTracker = activityTracker;
            _logFactory = logFactory;
        }

        /// <summary>
        /// Sign in a user
        /// </summary>
        /// <param name="usernameOrEmailAddress">Username or Email Address of the user.</param>
        public async void SignIn(string usernameOrEmailAddress) {
            var isValidEmail = CommonHelper.IsValidEmail(usernameOrEmailAddress);
            var user = isValidEmail ? _userAccountService.GetUserByEmail(usernameOrEmailAddress) : _userAccountService.GetUserByUsername(usernameOrEmailAddress);

            try {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.EmailAddress, ClaimTypes.Email, RaptorCookieAuthenticationDefaults.ClaimsIssuer),
                    new Claim(ClaimTypes.Name, user.DisplayName, ClaimValueTypes.String, RaptorCookieAuthenticationDefaults.ClaimsIssuer)
                };

                var userIdentity = new ClaimsIdentity(claims, RaptorCookieAuthenticationDefaults.AuthenticationScheme);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                var authenticationProperties = new AuthenticationProperties() {
                    IssuedUtc = DateTime.UtcNow
                };

                _activityTracker.InsertActivity(user.BusinessEntity, ActivityLogDefaults.LoggedIn, "User logged in");

                await _httpContextAccessor.HttpContext.SignInAsync(RaptorCookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);
            }
            catch (Exception ex) {
                _logFactory.InsertLog(LogLevel.Error, ex.Message, ex.ToString());
            }

            _cachedUser = user;
        }

        /// <summary>
        /// Signs out the currently logged in user
        /// </summary>
        public void SignOut() {
            _cachedUser = null;
            _httpContextAccessor.HttpContext.SignOutAsync(RaptorCookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Returns the currently logged in user
        /// </summary>
        /// <returns></returns>
        public Person GetAuthenticatedUser() {
            if (_cachedUser != null)
                return _cachedUser;

            var authenticationResult = _httpContextAccessor.HttpContext.AuthenticateAsync(RaptorCookieAuthenticationDefaults.AuthenticationScheme).Result;


            if (!authenticationResult.Succeeded)
                return null;

            Person user = null;
            var emailClaim = authenticationResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email);

            if (emailClaim != null)
                user = _userAccountService.GetUserByEmail(emailClaim.Value);

            if (user == null || user.IsDeleted || user.IsBlocked)
                return null;

            _cachedUser = user;

            user.DateLastLoginUtc = DateTime.UtcNow;
            _userAccountService.UpdateUser(user);

            return _cachedUser;

        }
    }
}
