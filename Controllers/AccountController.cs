using AccountService.Interface;
using AccountService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet("getaccountdetails")]
        public async Task<IEnumerable<Account>> GetAccountDetails()
        {
            return _accountManager.GetAccountDetails();
        }

        [HttpGet("getaccountdetailsById/{accountNumber}")]
        public async Task<Account> GetAccountDetailsById(Guid accountNumber)
        {
            return _accountManager.GetAccountDetailById(accountNumber);
        }

        [HttpPost("createaccount")]
        public async Task<Account> CreateAccount([FromBody] string customerId)
        {
            return await _accountManager.AddAccount(customerId);
        }

        [HttpDelete("deleteaccount/{customerId}")]
        public async Task<bool> DeleteAccount(string customerId)
        {
            return _accountManager.DeleteAccount(customerId);
        }
    }
}
