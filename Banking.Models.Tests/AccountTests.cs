namespace Banking.Models.Tests
{
    public class AccountTests
    {
        [Fact]
        public void ConstructorTests_NoInitialBalanceSpecified()
        {
            var target = new Account(new User(1, "Foo"));
            Assert.NotNull(target);
            Assert.Equal(0, target.Balance);
        }

        [Fact]
        public void ConstructorTests_BalanceSpecified()
        {
            var target = new Account(new User(1, "Foo"), 107.98M);
            Assert.NotNull(target);
            Assert.Equal(107.98M, target.Balance);
        }
    }
}