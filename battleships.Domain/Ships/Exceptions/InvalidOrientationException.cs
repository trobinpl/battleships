namespace battleships.Domain.Ships.Exceptions;

public class InvalidOrientationException : Exception
{
    public InvalidOrientationException() : base("Ship can be oriented either horizontally or vertically")
    {
    }
}
