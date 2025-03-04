namespace CafeSales.Middleware.ExceptionHandling.Errors;

public class CustomException : Exception
{
    public int StatusCode { get; set; }

    public string? InnerMessage { get; set; }

    public CustomException(string message, string innerMessage) : base(message)
    {
        InnerMessage = innerMessage;
    }

    public CustomException(string message, string innerMessage, Exception inner) : base(message, inner)
    {
        InnerMessage = innerMessage;
    }
}