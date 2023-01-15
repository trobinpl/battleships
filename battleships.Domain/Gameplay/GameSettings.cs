using battleships.Domain.Gameplay.ShipsGeneration;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay;

public class GameSettings
{
    private readonly Dictionary<Type, int> _defaultShipsRequirements = new Dictionary<Type, int>
    {
        [typeof(Carrier)] = 1,
        [typeof(Battleship)] = 2,
        [typeof(Cruiser)] = 3,
        [typeof(Destroyer)] = 4,
    };

    public int GridSize { get; init; }
    public Dictionary<Type, int> ShipsRequirements { get; init; }
    public IShipGenerationStrategy ComputerShipsGenerationStrategy { get; init; }
    public Random Random { get; init; } = new Random();

    public GameSettings(IShipGenerationStrategy computerShipsGenerationStrategy)
    {
        ComputerShipsGenerationStrategy = computerShipsGenerationStrategy;
        GridSize = 10;
        ShipsRequirements = _defaultShipsRequirements;
    }
}
