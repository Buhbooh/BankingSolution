using Banking.Api.Responses;
using Banking.Domain;
using Banking.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountProcessingService _processingService;

        public AccountsController(IAccountProcessingService processingService)
        {
            _processingService = processingService;
        }

        [HttpPost("/users/{userId}/accounts")]
        public async Task<IActionResult> CreateNewAccount(int userId, decimal deposit)
        {
            try
            {
                var account = await _processingService.CreateAccount(userId, deposit);
                return CreateCreatedAccountResponse(account, userId);

            } catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
            
        }

        [HttpPost("/accounts/{accountId}/deposit")]
        public async Task<IActionResult> Deposit(int accountId, decimal amount)
        {
            try
            {
                var result = await _processingService.Deposit(accountId, amount);
                return Ok(new AccountResponse(accountId, result));

            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }

        }

        [HttpPost("/accounts/{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, decimal amount)
        {
            try
            {
                var result = await _processingService.Withdraw(accountId, amount);
                return Ok(new AccountResponse(accountId, result));
            }
            catch (Exception ex)
            {
                
                return CreateErrorResponse(ex);
            }

        }

        [HttpDelete("/accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            try
            {
                await _processingService.DeleteAccount(accountId);

                return Ok(new BaseResponse() { Success = true, Error = String.Empty});

            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse() { Success = false, Error = ex.Message });
            }

        }

        private IActionResult CreateErrorResponse(Exception ex)
        {
            return Ok(new BaseResponse() { Error = ex.Message, Success = false });
        }

        private IActionResult CreateCreatedAccountResponse(Account account, int userId)
        {
            return Created($"/users/{userId}/accounts/{account.Id}", new AccountResponse(account.Id, account.Balance));
        }

    }
}
