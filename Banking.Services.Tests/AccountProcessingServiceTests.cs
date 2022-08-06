using Banking.Domain;
using Banking.Interfaces;
using Moq;

namespace Banking.Services.Tests
{
    public class AccountProcessingServiceTests
    {
        private readonly Mock<IAccountsDatastore> _mockAccountsDatastore;
        private readonly Mock<IUsersDatastore> _mockUsersDatastore;

        public AccountProcessingServiceTests()
        {
            _mockAccountsDatastore = new Mock<IAccountsDatastore>();
            _mockUsersDatastore = new Mock<IUsersDatastore>();
        }

        [Fact]
        public async void CreateAccountTest_BalanceLessThan100_ShouldThrowException()
        {
            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);

            await Assert.ThrowsAsync<Exception>(() => target.CreateAccount(1, 50));
        }

        [Fact]
        public async void CreateAccountTest_BalanceMoreThan10000_ShouldThrowException()
        {
            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);

            await Assert.ThrowsAsync<Exception>(() => target.CreateAccount(1, 12300));
        }

        [Fact]
        public async void CreateAccountTest_BalanceOf100_ShouldCreateAccount()
        {
            var stubUser = new User(1, "Foo");
            _mockUsersDatastore.Setup(p => p.GetUser(1)).ReturnsAsync(stubUser);

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);
            await target.CreateAccount(1, 100M);

            _mockAccountsDatastore.Verify(p => p.InsertNewAccount(stubUser, 100M));
        }

        [Fact]
        public async void CreateAccountTest_BalanceOfMoreThan100_ShouldCreateAccount()
        {
            var stubUser = new User(1, "Foo");
            _mockUsersDatastore.Setup(p => p.GetUser(1)).ReturnsAsync(stubUser);

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);
            await target.CreateAccount(1, 150M);

            _mockAccountsDatastore.Verify(p => p.InsertNewAccount(stubUser, 150M));
        }

        [Fact]
        public async void CreateAccountTest_BalanceOfExactly10000_ShouldCreateAccount()
        {
            var stubUser = new User(1, "Foo");
            _mockUsersDatastore.Setup(p => p.GetUser(1)).ReturnsAsync(stubUser);

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);
            await target.CreateAccount(1, 10000M);

            _mockAccountsDatastore.Verify(p => p.InsertNewAccount(stubUser, 10000M));
        }

        [Fact]
        public async void WithdrawTest_WhenAmountLessThan90Percent_ShouldReturnBalance()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 10000));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);
            var result = await target.Withdraw(100, 200);

            Assert.Equal(9800, result);
        }

        [Fact]
        public async void WithdrawTest_WhenAmountExactly90Percent_ShouldReturnBalance()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 10000));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);
            var result = await target.Withdraw(100, 9000);

            Assert.Equal(1000, result);
        }

        [Fact]
        public async void WithdrawTest_WhenAmountMoreThan90Percent_ShouldThrowException()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 10000));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);
         
            await Assert.ThrowsAsync<Exception>(() => target.Withdraw(100, 9100));
        }

        [Fact]
        public async void WithdrawTest_WhenWihdrawalWouldCauseLessThan100Balance_ShouldThrowException()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 120));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);

            await Assert.ThrowsAsync<Exception>(() => target.Withdraw(100, 21));
        }

        [Fact]
        public async void DepositTest_WhenDepositGreaterThan10000_ShouldThrowException()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 120));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);

            await Assert.ThrowsAsync<Exception>(() => target.Deposit(100, 12000));
        }

        [Fact]
        public async void DepositTest_WhenDepositExactly10000_ShouldReturnBalance()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 52000));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);

            var result = await target.Deposit(100, 10000);

            Assert.Equal(62000, result);
        }

        [Fact]
        public async void DepositTest_WhenDepositLessThan10000_ShouldReturnBalance()
        {
            _mockAccountsDatastore.Setup(p => p.GetAccount(100)).ReturnsAsync(new Account(100, new User(1, "FooUser"), 52000));

            var target = new AccountProcessingService(_mockUsersDatastore.Object, _mockAccountsDatastore.Object);

            var result = await target.Deposit(100, 10);

            Assert.Equal(52010, result);
        }
    }
}