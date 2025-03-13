using AccountService.Data;
using AccountService.Interface;
using AccountService.Model;
using System.Net.Http.Headers;
using System.Security.Principal;

namespace AccountService.BusinessLogic
{
    public class AccountManager : IAccountManager
    {
        public async Task<Account> AddAccount(string customerId)
        {
            var account = new Account()
            {
                AcccountNumber = Guid.NewGuid(),
                Amount = 0,
                CustomerId = customerId
            };

            var accountDetails = AccountData.accountList.FirstOrDefault(x => x.CustomerId == customerId);
            if (accountDetails == null)
            {
                // The API Gateway endpoint
                string apiGatewayUrl = $"https://localhost:7103/apigateway/customer/getcustomerdetailswithid/{customerId}";

                // Create an instance of HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Set the request headers
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your-access-token");

                    // Send a POST request to the API Gateway
                    var responseMessage = await client.GetAsync(apiGatewayUrl);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        AccountData.accountList.Add(account);
                    }
                    else
                    {
                        throw new Exception("Custome doesn't exist");
                    }
                }
                return AccountData.accountList.Where(x=>x.CustomerId == customerId).FirstOrDefault();
            }
            else
            {
                throw new Exception("Account already exist for given customerId");
            }
        }

        public bool DeleteAccount(string customerId)
        {
            var accountDetails = AccountData.accountList.FirstOrDefault(x => x.CustomerId == customerId);
            AccountData.accountList.Remove(accountDetails);
            return true;
        }

        public Account GetAccountDetailById(Guid AccountNumber)
        {
            var accountDetails = AccountData.accountList.FirstOrDefault(x => x.AcccountNumber == AccountNumber);
            if (accountDetails == null)
                throw new Exception("Not found");
            else
                return accountDetails;
        }

        public IEnumerable<Account> GetAccountDetails()
        {
                return AccountData.accountList;
        }
    }
}
