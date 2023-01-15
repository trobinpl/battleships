using battleships.Domain.Ships;

namespace battleships.Domain.Board;

public class Grid
{
    public int Size { get; init; }

    public List<Ship> Ships { get; init; } = new List<Ship>();

    public Grid(int size)
    {
        Size = size;
    }

    public void Place(Ship ship)
    {
        if (CollidesWithOtherShips(ship))
        {
            throw new ShipCollisionException(ship);
        }

        if (IsOutsideTheGrid(ship))
        {
            throw new ShipOutsideTheGridException(ship.Coordinates);
        }

        Ships.Add(ship);
    }

    public (ShootResult ShotResult, string HitShipName, ShipStatus ShipStatus) Shoot(Coordinate shotCoordinate)
    {
        var hitShip = Ships.Where(ship => ship.IsHit(shotCoordinate)).FirstOrDefault();

        if (hitShip is null)
        {
            return (ShootResult.Miss, string.Empty, ShipStatus.Unknown);
        }

        hitShip.DestroyAt(shotCoordinate);
        return (ShootResult.Hit, hitShip.Name, hitShip.Status);
    }

    internal Coordinate GetRandomCoordinate(Random random)
    {
        var randomColumn = (char)('A' + random.Next(Size));
        var randomRow = random.Next(Size) + 1;

        return new Coordinate(randomColumn, randomRow);
    }

    internal bool CanBePlaced(Ship ship) => !CollidesWithOtherShips(ship) && !IsOutsideTheGrid(ship);

    private HashSet<Coordinate> OccupiedCoordinates => Ships.SelectMany(ship => ship.Coordinates).ToHashSet();
    private bool CollidesWithOtherShips(Ship ship) => ship.Coordinates.Any(coordinate => OccupiedCoordinates.Contains(coordinate));
    private bool IsOutsideTheGrid(Coordinate coordinate) => coordinate.Column < 'A' || coordinate.Column > 'A' + Size || coordinate.Row < 1 || coordinate.Row > Size;
    private bool IsOutsideTheGrid(Ship ship) => ship.Coordinates.Any(coordinate => IsOutsideTheGrid(coordinate));

}
