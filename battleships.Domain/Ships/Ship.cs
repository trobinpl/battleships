using battleships.Domain.Board;
using battleships.Domain.Ships.Exceptions;

namespace battleships.Domain.Ships;

public abstract class Ship
{
    protected abstract int Size { get; }
    
    public abstract string Name { get; }
    
    public HashSet<Coordinate> Coordinates { get; init; } = new();
    
    public ShipStatus Status => _destroyedCoordinates.Count == Coordinates.Count ? ShipStatus.Sunk : ShipStatus.Afloat;
    
    private readonly HashSet<Coordinate> _destroyedCoordinates = new();

    public Ship(Coordinate startingPoint, ShipOrientation orientation)
    {
        Coordinates = GenerateCoordinates(startingPoint, orientation);
    }

    public bool IsHit(Coordinate shot) => Coordinates.Contains(shot) && !_destroyedCoordinates.Contains(shot);

    public void DestroyAt(Coordinate coordinate)
    {
        if (IsHit(coordinate))
        {
            _destroyedCoordinates.Add(coordinate);
        }
    }

    protected HashSet<Coordinate> GenerateCoordinates(Coordinate startingPoint, ShipOrientation orientation)
    {
        return orientation switch
        {
            ShipOrientation.Horizontal => GenerateHorizontal(Size, startingPoint),
            ShipOrientation.Vertical => GenerateVertical(Size, startingPoint),
            _ => throw new InvalidOrientationException()
        };
    }

    private static HashSet<Coordinate> GenerateVertical(int size, Coordinate startingPoint)
    {
        HashSet<Coordinate> coordinates = new() { startingPoint };

        for (int i = 1; i <= size - 1; i++)
        {
            coordinates.Add(new Coordinate((char)(startingPoint.Column + i), startingPoint.Row));
        }

        return coordinates;
    }

    private static HashSet<Coordinate> GenerateHorizontal(int size, Coordinate startingPoint)
    {
        HashSet<Coordinate> coordinates = new() { startingPoint };

        for (int i = 1; i <= size - 1; i++)
        {
            coordinates.Add(new Coordinate(startingPoint.Column, startingPoint.Row + i));
        }

        return coordinates;
    }

    public override int GetHashCode() => Coordinates.GetHashCode();

    public override string ToString() => $"{Name}: [{string.Join(", ", Coordinates)}]. {Status}";
}
