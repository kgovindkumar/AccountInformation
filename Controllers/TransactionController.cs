using AccountService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly IAccountManager _accountManager;
        private readonly ITransactionManager _transactionManager;

        public TransactionController(IAccountManager accountManager, ITransactionManager transactionManager)
        {
            _accountManager = accountManager;
            _transactionManager = transactionManager;
        }

        [HttpPost("addmoney/{accountId}")]
        public async Task<int> AddMoney(Guid accountId, [FromBody]  int amount)
        {
            return _transactionManager.AddMoney(accountId, amount);
        }

        [HttpPost("withdrawmoney/{accountId}")]
        public async Task<int> WithdrawMoney(Guid accountId, [FromBody]int amount)
        {
            return _transactionManager.WithDraw(accountId, amount);
        }
    }
}
