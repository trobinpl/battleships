using battleships.Domain.Board;

namespace battleships.Domain.Ships;

public class Battleship : Ship
{
    protected override int Size => 4;

    public override string Name => "Battleship";

    public Battleship(Coordinate startingPoint, ShipOrientation orientation) : base(startingPoint, orientation)
    {
    }
}
