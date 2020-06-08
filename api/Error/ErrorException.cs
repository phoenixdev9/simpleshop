namespace api.Error
{
    public class ErrorException : ErrorRes
    {
        public ErrorException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}