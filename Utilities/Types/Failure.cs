namespace Utilities.Types;

public abstract class TError
{
    public string ErrorMessage { get; protected set; }
}

public class Failure : TError
{
    public Failure(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}