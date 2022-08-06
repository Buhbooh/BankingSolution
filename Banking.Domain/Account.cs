namespace Banking.Domain
{
    public class Account    
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public decimal Balance { get; set; }

        public Account(int id, User owner, decimal balance = 0)
        {
            Id = id;
            Owner = owner;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            ValidateAmountIsPositive(amount);
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            ValidateAmountIsPositive(amount);

            if (amount > Balance)
            {
                throw new Exception("Cannot withdraw more than balance.");
            }

            Balance -= amount;
        }

        private void ValidateAmountIsPositive(decimal amount)
        {
            if (amount <= 0M)
            {
                throw new Exception("Amount must be greater than $0.");
            }
        }
    }
}