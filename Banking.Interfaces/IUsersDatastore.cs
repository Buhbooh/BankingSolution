using Banking.Domain;

namespace Banking.Interfaces
{
    public interface IUsersDatastore
    {
        Task<User> GetUser(int userId);
    }
}