namespace Raptor.Data.Models.Users
{

    public enum UserLoginResults
    {
        Successful = 1,
        UserNotExists = 2,
        WrongPassword = 3,
        NotActive = 4,
        Deleted = 5
    }
}
