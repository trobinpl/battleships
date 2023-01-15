using battleships.Domain.Gameplay.ShipsGeneration;

namespace battleships.Tests.Gameplay;

public class GameTests
{
    private readonly IShipGenerationStrategy _shipGenerationStrategy = new StaticShipsGenerationStrategy();

    [Fact]
    public void Game_MakeMove_GameIsWonWhenAllShipsDestroyed()
    {
        // Arrange
        var game = new Game(new GameSettings(_shipGenerationStrategy));
        var playerShips = new List<Ship>
        {
            new Carrier("A1", ShipOrientation.Horizontal),
            new Battleship("A10", ShipOrientation.Vertical),
            new Cruiser("D1", ShipOrientation.Vertical),
            new Destroyer("A7", ShipOrientation.Horizontal),
        };
        game.Prepare(playerShips);

        // Act
        (MoveResult playerFirstMoveResult, _) = game.MakeMove("A1");
        (MoveResult playerSecondMoveResult, _) = game.MakeMove("A2");

        // Assert
        playerFirstMoveResult.Result.ShootResult.Should().Be(ShootResult.Hit);
        playerFirstMoveResult.Result.HitShipName.Should().Be("Destroyer");
        playerFirstMoveResult.Result.HitShipStatus.Should().Be(ShipStatus.Afloat);
        playerFirstMoveResult.GameOver.Should().BeFalse();

        playerSecondMoveResult.Result.ShootResult.Should().Be(ShootResult.Hit);
        playerSecondMoveResult.Result.HitShipName.Should().Be("Destroyer");
        playerSecondMoveResult.Result.HitShipStatus.Should().Be(ShipStatus.Sunk);
        playerSecondMoveResult.GameOver.Should().BeTrue();
    }
}
