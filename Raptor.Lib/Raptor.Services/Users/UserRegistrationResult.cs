using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Users
{
    public class UserRegistrationResult
    {
        public IList<string> Errors { get; set; }

        public UserRegistrationResult()
        {
            Errors = new List<string>();
        }

        public bool Success => !Errors.Any();

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
