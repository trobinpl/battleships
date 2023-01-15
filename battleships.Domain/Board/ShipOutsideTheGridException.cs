namespace battleships.Domain.Board;

public class ShipOutsideTheGridException : Exception
{
    public ShipOutsideTheGridException(IEnumerable<Coordinate> coordinates) : base($"Ship with coordinates [{string.Join(',', coordinates)}] lays outside the generated grid")
    {
    }
}
