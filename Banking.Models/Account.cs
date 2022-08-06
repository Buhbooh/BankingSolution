namespace Banking.Models
{
    public class Account    
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public decimal Balance { get; set; }

        public Account(User owner, decimal balance = 0)
        {
            Owner = owner;
            Balance = balance;
        }
    }
}