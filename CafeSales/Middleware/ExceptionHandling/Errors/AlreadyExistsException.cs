namespace CafeSales.Middleware.ExceptionHandling.Errors;

public class AlreadyExistsException : CustomException
{
    public AlreadyExistsException(string message, string innerMessage) : base(message, innerMessage)
    {
        StatusCode = 400;
    }
}