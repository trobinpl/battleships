using battleships.Domain.Board;

namespace battleships.Domain.Ships;

public class Carrier : Ship
{
    protected override int Size => 5;

    public override string Name => "Carrier";

    public Carrier(Coordinate startingPoint, ShipOrientation orientation) : base(startingPoint, orientation)
    {
    }
}
