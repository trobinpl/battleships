namespace battleships.Domain.Ships;

public enum ShipOrientation
{
    Unknown = 0,
    Horizontal = 1,
    Vertical = 2,
}

public static class ShipOrientationExtensions
{
    public static ShipOrientation GetRandom()
    {
        var availableValues = new List<ShipOrientation> { ShipOrientation.Horizontal, ShipOrientation.Vertical };
        var random = new Random();
        return availableValues[random.Next(availableValues.Count)];
    }
}
