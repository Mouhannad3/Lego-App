public class LegoException : Exception
{
    public LegoException()
    {
    }

    public LegoException(string? message) : base(message)
    {
    }

    public LegoException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}