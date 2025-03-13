using AccountService.Data;
using AccountService.Interface;
using AccountService.Model;

namespace AccountService.BusinessLogic
{
    public class TransactionManager : ITransactionManager
    {

        public int AddMoney(Guid accountId, int amount)
        {
            var accountDetails = AccountData.accountList.FirstOrDefault(x => x.AcccountNumber == accountId);
            if (accountDetails != null)
            {
                accountDetails.Amount = accountDetails.Amount + amount;
                AccountData.accountList = AccountData.accountList
                    .Select(x => x.AcccountNumber == accountDetails.AcccountNumber ? accountDetails : x)
                    .ToList();
                return accountDetails.Amount;
            }
            throw new Exception("Account not exist");
        }

        public int WithDraw(Guid accountId, int amount)
        {
            var accountDetails = AccountData.accountList.FirstOrDefault(x => x.AcccountNumber == accountId);
            if (accountDetails != null)
            {
                accountDetails.Amount = accountDetails.Amount - amount;
                if (accountDetails.Amount < 0)
                {
                    throw new Exception("Withdraw Amount should be less than bank amount");
                }
                AccountData.accountList = AccountData.accountList
                    .Select(x => x.AcccountNumber == accountDetails.AcccountNumber ? accountDetails : x)
                    .ToList();
                return accountDetails.Amount;
            }
            throw new Exception("Account not exist");
        }
    }
}
