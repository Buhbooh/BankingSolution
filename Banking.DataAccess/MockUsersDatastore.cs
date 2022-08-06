using Banking.Domain;
using Banking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.DataAccess
{
    public class MockUsersDatastore : IUsersDatastore
    {

        private Dictionary<int, User> Users { get; }

        public MockUsersDatastore()
        {
            Users = new Dictionary<int, User>();
            AddStartingDummyUsers();
        }

        public async Task<User> GetUser(int userId)
        {
            if (Users.ContainsKey(userId))
            {
                return await Task.FromResult(Users[userId]);
            }

            throw new Exception("User doesn't exist.");
        }

        private void AddStartingDummyUsers()
        {
            Users.Add(1, new User(1, "Drew Brees"));
            Users.Add(2, new User(2, "Bum Phillips"));
            Users.Add(3, new User(3, "Ricky Jackson"));
        }
    }
}
