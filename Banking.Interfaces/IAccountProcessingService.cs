using Banking.Domain;

namespace Banking.Interfaces
{
    public interface IAccountProcessingService
    {
        Task<Account> CreateAccount(int userId, decimal balance);
        Task DeleteAccount(int accountId);
        Task<decimal> Deposit(int accountId, decimal amount);
        Task<decimal> Withdraw(int accountId, decimal amount);
    }
}