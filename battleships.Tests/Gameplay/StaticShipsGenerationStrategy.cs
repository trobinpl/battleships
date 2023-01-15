using battleships.Domain.Gameplay.ShipsGeneration;
using battleships.Domain.Ships;

namespace battleships.Tests.Gameplay;

internal class StaticShipsGenerationStrategy : IShipGenerationStrategy
{
    public IEnumerable<Ship> GenerateShips(Dictionary<Type, int> shipsRequirements, Grid grid) => new List<Ship>
    {
        new Destroyer("A1", ShipOrientation.Horizontal),
    };
}
