namespace Banking.Api.Responses
{
    public class BaseResponse
    {
        public string Error { get; set; }
        public bool Success { get; set; }

        public BaseResponse()
        {
            Error = String.Empty;
            Success = true;
        }

        public BaseResponse(string error, bool success)
        {
            Error = error;
            Success = success;
        }
    }
}
