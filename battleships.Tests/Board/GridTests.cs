namespace battleships.Tests.Board;

public class GridTests
{
    [Fact]
    public void Grid_PlaceShip_ShipIsOnTheGrid()
    {
        // Arrange
        var grid = new Grid(10);
        var ship = new Carrier("B6", ShipOrientation.Horizontal);

        // Act
        grid.Place(ship);

        // Assert
        grid.Ships.Should().Contain(ship);
    }

    [Fact]
    public void Grid_PlaceShipOnOccupiedSpot_ThrowsShipCollisionException()
    {
        // Arrange
        var grid = new Grid(10);
        var ship = new Carrier("A1", ShipOrientation.Horizontal);
        var anotherShip = new Cruiser("A4", ShipOrientation.Vertical);
        grid.Place(ship);

        // Act
        Action placeAnotherShip = () => grid.Place(anotherShip);

        // Assert
        placeAnotherShip.Should().ThrowExactly<ShipCollisionException>();
    }

    [Fact]
    public void Grid_PlaceShipOutsideTheGrid_ThrowsShipOutsideTheGridException()
    {
        // Arrange
        var grid = new Grid(10);
        var ship = new Carrier("A7", ShipOrientation.Horizontal);

        // Act
        Action placeShip = () => grid.Place(ship);

        // Assert
        placeShip.Should().ThrowExactly<ShipOutsideTheGridException>();
    }

    [Theory]
    [InlineData("D3", "D3", "Hit", "Carrier")]
    [InlineData("D3", "E3", "Hit", "Carrier")]
    [InlineData("D3", "F3", "Hit", "Carrier")]
    [InlineData("D3", "G3", "Hit", "Carrier")]
    [InlineData("D3", "H3", "Hit", "Carrier")]
    public void Grid_ShootAtShip_ReturnsCorrectShootResult(string shipStartingPosition, string shootCoordinate, string expectedShootResult, string expectedShipHit)
    {
        // Arrange
        var grid = new Grid(10);
        var ship = new Carrier(shipStartingPosition, ShipOrientation.Vertical);
        grid.Place(ship);

        // Action
        (ShootResult shootResult, string shipHit, ShipStatus shipStatus) = grid.Shoot(shootCoordinate);

        // Assert
        shootResult.Should().Be(Enum.Parse<ShootResult>(expectedShootResult));
        shipHit.Should().Be(expectedShipHit);
        shipStatus.Should().Be(ShipStatus.Afloat);
    }

    [Theory]
    [InlineData("D3", false, "D3", "G3")]
    [InlineData("D3", false, "D3", "G3", "F3")]
    [InlineData("D3", true, "D3", "E3", "F3", "G3", "H3")]
    public void Grid_ShootAtShip_ReturnsCorrectShipStatus(string shipStartingPosition, bool shouldBeDestroyed, params string[] shots)
    {
        // Arrange
        var grid = new Grid(10);
        var ship = new Carrier(shipStartingPosition, ShipOrientation.Vertical);
        var shootingResults = new List<ShipStatus>();
        var exptectedShipStatus = shouldBeDestroyed ? ShipStatus.Sunk : ShipStatus.Afloat;
        grid.Place(ship);

        // Action
        foreach (var shot in shots)
        {
            (_, _, ShipStatus shipStatus) = grid.Shoot(shot);
            shootingResults.Add(shipStatus);
        }

        // Assert
        shootingResults.SkipLast(1).Should().AllSatisfy(status => status.Should().Be(ShipStatus.Afloat));
        shootingResults.Last().Should().Be(exptectedShipStatus);
    }
}
