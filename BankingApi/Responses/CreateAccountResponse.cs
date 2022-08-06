namespace Banking.Api.Responses
{
    public class AccountResponse : BaseResponse
    {
        public int AccountNo { get; set; }
        public decimal Balance { get; set; }
      

        public AccountResponse() : base()
        {
         
        }

        public AccountResponse(int accountNo, decimal balance) : base()
        {
            AccountNo = accountNo;
            Balance = balance;
        }

    }
}
