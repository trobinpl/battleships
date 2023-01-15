using battleships.Domain.Board;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay;

public class Game
{
    public bool IsOver => _players.Values.Any(player => player.Lost);

    public PlayerType Winner { get; private set; } = PlayerType.Unknown;

    private readonly Dictionary<PlayerType, Player> _players;

    private readonly GameSettings _gameSettings;

    public Game(GameSettings settings)
    {
        _gameSettings = settings;
        _players = new Dictionary<PlayerType, Player>
        {
            [PlayerType.Human] = new Player(new Grid(settings.GridSize)),
            [PlayerType.Computer] = new Player(new Grid(settings.GridSize)),
        };
    }

    public void Prepare(IEnumerable<Ship> playerShips)
    {
        if (playerShips.Any(ship => !_players[PlayerType.Human].Grid.CanBePlaced(ship)))
        {
            throw new CantPrepareGameException("Can't place one or more player ships on the grid");
        }

        foreach (var ship in playerShips)
        {
            _players[PlayerType.Human].Grid.Place(ship);
        }

        foreach (var ship in _gameSettings.ComputerShipsGenerationStrategy.GenerateShips(_gameSettings.ShipsRequirements, _players[PlayerType.Computer].Grid))
        {
            _players[PlayerType.Computer].Grid.Place(ship);
        }
    }

    public MoveResult MakeMove(Coordinate shotCoordinate)
    {
        var moveAgainst = _players[PlayerType.Computer];

        var playerShootResult = moveAgainst.Shoot(shotCoordinate);

        if (moveAgainst.Lost)
        {
            Winner = PlayerType.Human;
        }
        else
        {
            MakeComputerMove();
            if (_players[PlayerType.Human].Lost)
            {
                Winner = PlayerType.Computer;
            }
        }

        return new MoveResult(playerShootResult, moveAgainst.Lost);
    }
    private void MakeComputerMove()
    {
        _players[PlayerType.Human].Shoot(_players[PlayerType.Human].Grid.GetRandomCoordinate(_gameSettings.Random));
    }
}
