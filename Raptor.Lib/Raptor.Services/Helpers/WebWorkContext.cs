using Raptor.Data.Models.Configuration;
using Raptor.Data.Models.Users;
using Raptor.Services.Authentication;

namespace Raptor.Services.Helpers
{
    public class WebWorkContext : IWorkContext
    {
        private readonly IUserAuthenticationService _authService;

        private Person _cachedUser;

        public WebWorkContext(IUserAuthenticationService authService) {
            _authService = authService;
        }

        public Person CurrentUser
        {
            get {

                if (_cachedUser != null) return _cachedUser;

                _cachedUser = _authService.GetAuthenticatedUser();
                return _cachedUser;
            }

            set {
                _cachedUser = value;
            }
        }

        public bool IsAdmin { get; set; }

        public SettingValues Settings { get; set; }
    }
}
