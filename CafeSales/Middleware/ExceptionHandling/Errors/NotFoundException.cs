namespace CafeSales.Middleware.ExceptionHandling.Errors;

public class NotFoundException : CustomException
{
    public NotFoundException(string message, string innerMessage) : base(message, innerMessage)
    {
        StatusCode = 400;
    }
}