using battleships.Domain.Board;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay.ShipsGeneration;

public interface IShipGenerationStrategy
{
    IEnumerable<Ship> GenerateShips(Dictionary<Type, int> shipsRequirements, Grid grid);
}
