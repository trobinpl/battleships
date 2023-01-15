using battleships.Domain.Board;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay;

public class Player
{
    internal Grid Grid { get; init; }

    public bool Lost => Grid.Ships.All(ship => ship.Status == ShipStatus.Sunk);

    public Player(Grid grid)
    {
        Grid = grid;
    }

    internal (ShootResult shootResult, string HitShipName, ShipStatus HitShipStatus) Shoot(Coordinate coordinate) => Grid.Shoot(coordinate);
}
