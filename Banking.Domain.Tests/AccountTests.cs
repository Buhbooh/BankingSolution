using Banking.Domain;

namespace Banking.Models.Tests
{
    public class AccountTests
    {
        [Fact]
        public void ConstructorTest_NoInitialBalanceSpecified()
        {
            var target = new Account(1, new User(1, "Foo"));
            Assert.NotNull(target);
            Assert.Equal(0, target.Balance);
        }

        [Fact]
        public void ConstructorTest_BalanceSpecified()
        {
            var target = new Account(1, new User(1, "Foo"), 107.98M);
            Assert.NotNull(target);
            Assert.Equal(107.98M, target.Balance);
        }

        [Fact]
        public void DepositTest_BalanceUpdated()
        {
            var target = new Account(1, new User(1, "Foo"), 107.98M);
            target.Deposit(50M);

            Assert.Equal(157.98M, target.Balance);
        }

        [Fact]
        public void DepositTest_WhenNegative_ThrowException()
        {
            var target = new Account(1, new User(1, "Foo"), 107.98M);
          
            Assert.Throws<Exception>(() => target.Deposit(-2M));

        }

        [Fact]
        public void DepositTest_WhenZero_ThrowException()
        {
            var target = new Account(1, new User(1, "Foo"), 107.98M);

            Assert.Throws<Exception>(() => target.Deposit(0M));

        }
    }
}