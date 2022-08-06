using Banking.Domain;
using Banking.Interfaces;

namespace Banking.DataAccess
{

    public class MockAccountsDatastore : IAccountsDatastore
    {
        private int _lastUsedAccountId;

        private Dictionary<int, Account> Accounts { get; }
    
        private IUsersDatastore _usersDatastore;

        public MockAccountsDatastore(IUsersDatastore usersDatastore)
        {
            Accounts = new Dictionary<int, Account>();
            _lastUsedAccountId = 103;
            _usersDatastore = usersDatastore;

            AddStartingDummyAccounts();
        }

        public async Task<Account> InsertNewAccount(User owner, decimal balance)
        {
            _lastUsedAccountId += 1;

            var account = new Account(_lastUsedAccountId, owner, balance);
            Accounts.Add(_lastUsedAccountId, account);
            return await Task.FromResult(account);
        }

        public async Task DeleteAccount(int accountId)
        {
            if (!Accounts.ContainsKey(accountId))
            {
                throw new Exception("Account doesn't exist.");
            }

            Accounts.Remove(accountId);
            await Task.FromResult(0);
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            if (Accounts.ContainsKey(account.Id))
            {
                Accounts[account.Id] = account;
                return await Task.FromResult(true);
            }

            throw new Exception("Account doesn't exist.");
        }

        public async Task<Account> GetAccount(int id)
        {
            if (Accounts.ContainsKey(id))
            {
                return await Task.FromResult(Accounts[id]);
            }

            throw new Exception("Account doesn't exist.");
        }

        private void AddStartingDummyAccounts()
        {
            var user1Task = _usersDatastore.GetUser(1);
            var user2Task = _usersDatastore.GetUser(2);
            var user3Task = _usersDatastore.GetUser(3);

            Task.WaitAll(user1Task, user2Task, user3Task);

            Accounts.Add(100, new Account(100, user1Task.Result, 100));
            Accounts.Add(101, new Account(101, user1Task.Result, 2500));
            Accounts.Add(102, new Account(102, user2Task.Result, 150));
            Accounts.Add(103, new Account(103, user3Task.Result, 450));
        }
    }
}