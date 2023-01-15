using battleships.Domain.Gameplay.ShipsGeneration;
using battleships.Domain.Ships;
using Microsoft.VisualStudio.CodeCoverage;

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
        var firstMoveResult = game.MakeMove("A1");
        var secondMoveResult = game.MakeMove("A2");

        // Assert
        firstMoveResult.Result.ShootResult.Should().Be(ShootResult.Hit);
        firstMoveResult.Result.HitShipName.Should().Be("Destroyer");
        firstMoveResult.Result.HitShipStatus.Should().Be(ShipStatus.Afloat);
        firstMoveResult.GameOver.Should().BeFalse();

        secondMoveResult.Result.ShootResult.Should().Be(ShootResult.Hit);
        secondMoveResult.Result.HitShipName.Should().Be("Destroyer");
        secondMoveResult.Result.HitShipStatus.Should().Be(ShipStatus.Sunk);
        secondMoveResult.GameOver.Should().BeTrue();
    }
}
