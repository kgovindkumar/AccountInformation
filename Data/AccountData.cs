using AccountService.Model;

namespace AccountService.Data
{
    public static class AccountData
    {
        public static List<Account> accountList = new List<Account>()
        {
            new Account()
            {
                AcccountNumber = Guid.NewGuid(),
                Amount = 0,
                CustomerId = "11111111"
            },
            new Account()
            {
                AcccountNumber = Guid.NewGuid(),
                Amount = 0,
                CustomerId = "22222222"
            }
        };
    }
}
