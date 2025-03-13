using AccountService.Model;

namespace AccountService.Interface
{
    public interface IAuthenticationManager
    {
        string Authenticate(UserDetail userDetail);
    }
}
