using battleships.Domain.Board;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay.ShipsGeneration;

public class RandomShipsGenerationStrategy : IShipGenerationStrategy
{
    private readonly Random _random;

    public RandomShipsGenerationStrategy(Random? random = null)
    {
        _random = random ?? new Random();
    }

    public IEnumerable<Ship> GenerateShips(Dictionary<Type, int> shipsRequirements, Grid grid)
    {
        foreach (var shipToPlace in shipsRequirements)
        {
            for (int i = 0; i < shipToPlace.Value; i++)
            {
                yield return GetRandomShip(grid, shipToPlace.Key);
            }
        }
    }

    private Ship GetRandomShip(Grid grid, Type shipType)
    {
        int tries = 0;
        Ship ship;
        do
        {
            if (tries > 1000)
            {
                throw new CantPrepareGameException("Can't place one or more computer ships on the grid");
            }

            var randomStartingCoordinate = grid.GetRandomCoordinate(_random);
            var randomShipOrientation = GetRandomShipOrientation();
            ship = (Ship)Activator.CreateInstance(shipType, randomStartingCoordinate, randomShipOrientation)!;
            tries++;
        }
        while (!grid.CanBePlaced(ship));

        return ship;
    }

    private ShipOrientation GetRandomShipOrientation()
    {
        var availableValues = new List<ShipOrientation> { ShipOrientation.Horizontal, ShipOrientation.Vertical };
        return availableValues[_random.Next(availableValues.Count)];
    }
}
