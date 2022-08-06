using Banking.Domain;

namespace Banking.Interfaces
{
    public interface IAccountsDatastore
    {
        Task DeleteAccount(int accountId);
        Task<Account> GetAccount(int id);
        Task<Account> InsertNewAccount(User owner, decimal balance);
        Task<bool> UpdateAccount(Account account);
    }
}