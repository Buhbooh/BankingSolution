using Banking.DataAccess;
using Banking.Domain;
using Banking.Interfaces;

namespace Banking.Services
{
    public class AccountProcessingService : IAccountProcessingService
    {
        protected IUsersDatastore _usersDatastore;
        protected IAccountsDatastore _accountsDatastore;

        protected const decimal MinimumBalance = 100M;
        protected const decimal MaxDeposit = 10000M;
        protected const decimal MaxWithdrawPercentage = 0.9M;


        public AccountProcessingService(IUsersDatastore usersData, IAccountsDatastore accountsData)
        {
            _usersDatastore = usersData;
            _accountsDatastore = accountsData;
        }

        public async Task<Account> CreateAccount(int userId, decimal balance)
        {
            User user = await _usersDatastore.GetUser(userId);

            ValidateMinimumBalance(balance);
            ValidateMaxDeposit(balance);

            return await _accountsDatastore.InsertNewAccount(user, balance);
        }

        public async Task DeleteAccount(int accountId)
        {
            await _accountsDatastore.DeleteAccount(accountId);
        }

        public async Task<decimal> Withdraw(int accountId, decimal amount)
        {
            var account = await _accountsDatastore.GetAccount(accountId);

            if (amount > (account.Balance * MaxWithdrawPercentage))
            {
                throw new Exception("You cannot withdraw more than 90% of your current balance.");
            }

            ValidateMinimumBalance(account.Balance - amount);

            // TODO: sync these 2 statements
            account.Withdraw(amount);
            await _accountsDatastore.UpdateAccount(account);

            return account.Balance;
        }

        public async Task<decimal> Deposit(int accountId, decimal amount)
        {
            var account = await _accountsDatastore.GetAccount(accountId);

            ValidateMaxDeposit(amount);

            // TODO: sync these 2 statements
            account.Deposit(amount);
            await _accountsDatastore.UpdateAccount(account);

            return account.Balance;
        }

        private static void ValidateMaxDeposit(decimal deposit)
        {
            if (deposit > MaxDeposit)
            {
                throw new Exception("Cannot deposit more than $10000.");
            }
        }

        private static void ValidateMinimumBalance(decimal balance)
        {
            if (balance < MinimumBalance)
            {
                throw new Exception("Must have a balance of at least $100.");
            }
        }
    }
}