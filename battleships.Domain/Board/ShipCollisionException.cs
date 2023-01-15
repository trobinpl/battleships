using battleships.Domain.Ships;

namespace battleships.Domain.Board;

public class ShipCollisionException : Exception
{
    public ShipCollisionException(Ship newShip) : base($"Ship with coordindates [{newShip}] collides with another ship already on the grid")
    {
    }
}
