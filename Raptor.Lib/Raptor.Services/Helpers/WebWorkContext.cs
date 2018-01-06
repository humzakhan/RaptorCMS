using Raptor.Data.Models.Users;
using Raptor.Services.Authentication;
using Raptor.Services.Users;

namespace Raptor.Services.Helpers
{
    public class WebWorkContext : IWorkContext
    {
        private readonly IUserService _userService;
        private readonly IUserAuthenticationService _authService;

        private Person _cachedUser;

        public WebWorkContext(IUserService userService, IUserAuthenticationService authService) {
            _userService = userService;
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
    }
}
