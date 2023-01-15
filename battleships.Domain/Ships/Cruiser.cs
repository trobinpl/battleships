using battleships.Domain.Board;
namespace battleships.Domain.Ships;

public class Cruiser : Ship
{
    protected override int Size => 3;

    public override string Name => "Cruiser";

    public Cruiser(Coordinate startingPoint, ShipOrientation orientation) : base(startingPoint, orientation)
    {
    }
}
