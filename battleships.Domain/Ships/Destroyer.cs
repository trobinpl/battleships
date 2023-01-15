using battleships.Domain.Board;
namespace battleships.Domain.Ships;

public class Destroyer : Ship
{
    protected override int Size => 2;

    public override string Name => "Destroyer";

    public Destroyer(Coordinate startingPoint, ShipOrientation orientation) : base(startingPoint, orientation)
    {
    }
}
