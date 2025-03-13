using AccountService.Model;

namespace AccountService.Interface
{
    public interface ITransactionManager
    {
        int AddMoney(Guid accountId, int amount);
        int WithDraw(Guid accountId, int amount);
    }
}
