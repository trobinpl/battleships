namespace battleships.Domain.Gameplay;

public class CantPrepareGameException : Exception
{
    public CantPrepareGameException(string? message) : base(message)
    {
    }
}
