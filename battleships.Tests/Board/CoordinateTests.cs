namespace battleships.Tests.Board;

public class CoordinateTests
{
    [Fact]
    public void Coordinate_EqualsWithNull_ReturnsProperValue()
    {
        // Arrange
        Coordinate a = new('A', 6);
        Coordinate? b = null;

        // Act
        bool equals = a.Equals(b);

        // Assert
        equals.Should().BeFalse();
    }

    [Theory]
    [InlineData("A5", 'A', 5)]
    [InlineData("Z10", 'Z', 10)]
    [InlineData("c10", 'C', 10)]
    public void Coordinate_FromString_ReturnsProperValue(string position, char expectedColumn, int expectedRow)
    {
        // Arrange & Act
        Coordinate gridPosition = position;

        gridPosition.Column.Should().Be(expectedColumn);
        gridPosition.Row.Should().Be(expectedRow);
    }

    [Theory]
    [InlineData("A1", "A1", true)]
    [InlineData("A1", "C1", false)]
    public void Coordinate_Equals_ReturnsCorrectValue(string a, string b, bool shouldBeEqual)
    {
        // Arrange
        Coordinate coordinateA = a;
        Coordinate coordinateB = b;

        // Act
        var equalResult = coordinateA == coordinateB;
        var notEqualResult = coordinateA != coordinateB;

        // Assert
        equalResult.Should().Be(shouldBeEqual);
        notEqualResult.Should().Be(!shouldBeEqual);
    }

    [Fact]
    public void Coordinate_ToString_ReturnsConcatenatedValue()
    {
        // Arrange
        var gridPosition = new Coordinate('F', 6);

        // Act
        string position = gridPosition.ToString();

        // Assert
        position.Should().Be("F6");
    }
}
