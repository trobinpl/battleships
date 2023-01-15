namespace battleships.Tests.Ships;

public class ShipTests
{
    [Theory]
    [InlineData(typeof(Destroyer), "Horizontal", "A5,A6")]
    [InlineData(typeof(Destroyer), "Vertical", "A5,B5")]
    [InlineData(typeof(Cruiser), "Horizontal", "A5,A6,A7")]
    [InlineData(typeof(Cruiser), "Vertical", "A5,B5,C5")]
    [InlineData(typeof(Battleship), "Horizontal", "A5,A6,A7,A8")]
    [InlineData(typeof(Battleship), "Vertical", "A5,B5,C5,D5")]
    [InlineData(typeof(Carrier), "Horizontal", "A5,A6,A7,A8,A9")]
    [InlineData(typeof(Carrier), "Vertical", "A5,B5,C5,D5,E5")]
    public void Ship_CreateNew_IsCreatedWithProperValues(Type shipToCreate, string orientationString, string expectedCoordinates)
    {
        // Arrange
        var startingPoint = (Coordinate)"A5";
        var orientation = Enum.Parse<ShipOrientation>(orientationString);
        var shipCoordinates = expectedCoordinates.Split(',').Select(coordinate => (Coordinate)coordinate);

        // Act
        var ship = (Ship)Activator.CreateInstance(shipToCreate, startingPoint, orientation)!;

        // Assert
        ship.Coordinates.Should().ContainInConsecutiveOrder(shipCoordinates);
    }

    [Theory]
    [InlineData(typeof(Destroyer), "A1", "A2", true)]
    [InlineData(typeof(Destroyer), "A1", "A4", false)]
    [InlineData(typeof(Cruiser), "A1", "A3", true)]
    [InlineData(typeof(Cruiser), "A1", "A4", false)]
    [InlineData(typeof(Battleship), "A1", "A4", true)]
    [InlineData(typeof(Battleship), "A1", "A5", false)]
    [InlineData(typeof(Carrier), "A1", "A5", true)]
    [InlineData(typeof(Carrier), "A1", "A6", false)]
    public void Ship_IsHit_DetectsHitProperly(Type shipToCreate, string startingPoint, string shootCoordinate, bool shouldBeHit)
    {
        // Arrange
        var ship = (Ship)Activator.CreateInstance(shipToCreate, (Coordinate)startingPoint, ShipOrientation.Horizontal)!;

        // Act
        var isHit = ship.IsHit(shootCoordinate);

        // Assert
        isHit.Should().Be(shouldBeHit);
    }

    [Theory]
    [InlineData(typeof(Destroyer), "A1", false, "A1")]
    [InlineData(typeof(Destroyer), "A1", true, "A1", "A2")]
    [InlineData(typeof(Cruiser), "A1", false, "A1")]
    [InlineData(typeof(Cruiser), "A1", true, "A1", "A2", "A3")]
    [InlineData(typeof(Battleship), "A1", false, "A1")]
    [InlineData(typeof(Battleship), "A1", true, "A1", "A2", "A3", "A4")]
    [InlineData(typeof(Carrier), "A1", false, "A1")]
    [InlineData(typeof(Carrier), "A1", true, "A1", "A2", "A2", "A3", "A4", "A5")]
    public void Ship_Destroy_CorrectShipStatus(Type shipToCreate, string startingPoint, bool shouldBeDestroyed, params string[] shots)
    {
        // Arrange
        var expectedShipStatus = shouldBeDestroyed ? ShipStatus.Sunk : ShipStatus.Afloat;
        var ship = (Ship)Activator.CreateInstance(shipToCreate, (Coordinate)startingPoint, ShipOrientation.Horizontal)!;

        // Act
        foreach (var shot in shots)
        {
            ship.DestroyAt(shot);
        }

        // Assert
        ship.Status.Should().Be(expectedShipStatus);
    }
}