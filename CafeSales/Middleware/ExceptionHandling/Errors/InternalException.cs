namespace CafeSales.Middleware.ExceptionHandling.Errors;

public class InternalException : CustomException
{
    public InternalException(string innerMessage, Exception inner) : base("Internal Server Error", innerMessage, inner)
    {
        StatusCode = 500;
    }
}