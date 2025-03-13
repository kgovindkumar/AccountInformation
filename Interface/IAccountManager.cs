using AccountService.Model;

namespace AccountService.Interface
{
    public interface IAccountManager
    {
        Task<Account> AddAccount(string customerId);
        bool DeleteAccount(string customerId);
        Account GetAccountDetailById(Guid AccountNumber);
        IEnumerable<Account> GetAccountDetails();
    }
}
