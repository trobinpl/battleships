namespace battleships.Tests.Gameplay;

public class GameSettingsTests
{
    [Fact]
    public void GameSettings_CreateNewObject_CorrectDefaultValuesSet()
    {
        // Arrange & Act
        var gameSettings = new GameSettings(new StaticShipsGenerationStrategy());

        // Assert
        gameSettings.GridSize.Should().Be(10);
        gameSettings.ShipsRequirements[typeof(Carrier)].Should().Be(1);
        gameSettings.ShipsRequirements[typeof(Battleship)].Should().Be(2);
        gameSettings.ShipsRequirements[typeof(Cruiser)].Should().Be(3);
        gameSettings.ShipsRequirements[typeof(Destroyer)].Should().Be(4);
    }
}