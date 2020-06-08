namespace api.Error
{
    public class ErrorRes
    {
        public ErrorRes(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            var sorry = ", sorry!";
            return statusCode switch
            {
                400 => "Bad Request" + sorry,
                401 => "You are Unauthorized" + sorry,
                404 => "Item that you are looking for is not found" + sorry,
                500 => "Something wrong with our server" + sorry,
                _ => null
            };
        }
    }
}