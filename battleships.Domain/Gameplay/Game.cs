using battleships.Domain.Board;
using battleships.Domain.Ships;

namespace battleships.Domain.Gameplay;

public class Game
{
    public bool IsOver => _players.Values.Any(player => player.Lost);

    public PlayerType Winner { get; private set; } = PlayerType.Unknown;

    private readonly Dictionary<PlayerType, Player> _players;

    public GameSettings Settings { get; private set; }

    public Game(GameSettings settings)
    {
        Settings = settings;
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

        foreach (var ship in Settings.ComputerShipsGenerationStrategy.GenerateShips(Settings.ShipsRequirements, _players[PlayerType.Computer].Grid))
        {
            _players[PlayerType.Computer].Grid.Place(ship);
        }
    }

    public (MoveResult PlayerMoveResult, MoveResult? ComputerMoveResult) MakeMove(Coordinate shotCoordinate)
    {
        var moveAgainst = _players[PlayerType.Computer];

        var playerShootResult = moveAgainst.Shoot(shotCoordinate);
        var playerMoveResult = new MoveResult(shotCoordinate, playerShootResult, moveAgainst.Lost);
        MoveResult? computerMoveResult = null;

        if (moveAgainst.Lost)
        {
            Winner = PlayerType.Human;
        }
        else
        {
            computerMoveResult = MakeComputerMove();
            if (_players[PlayerType.Human].Lost)
            {
                Winner = PlayerType.Computer;
            }

        }

        return (playerMoveResult, computerMoveResult);
    }
    private MoveResult MakeComputerMove()
    {
        var moveAgainst = _players[PlayerType.Human];
        var shotCoordinate = moveAgainst.Grid.GetRandomCoordinate(Settings.Random);
        var shootResult = moveAgainst.Shoot(shotCoordinate);
        return new MoveResult(shotCoordinate, shootResult, moveAgainst.Lost);
    }
}
