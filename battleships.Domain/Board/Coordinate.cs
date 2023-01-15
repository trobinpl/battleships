namespace battleships.Domain.Board;

public struct Coordinate : IEquatable<Coordinate>
{
    public char Column { get; set; }
    public int Row { get; set; }

    public Coordinate(char column, int row)
    {
        Column = column;
        Row = row;
    }

    public bool Equals(Coordinate other) => other.Column == Column && other.Row == Row;

    public static implicit operator Coordinate(string position) => new(char.ToUpperInvariant(position[0]), int.Parse(position[1..]));

    public override int GetHashCode() => HashCode.Combine(Column, Row);

    public override bool Equals(object? obj) => obj is not null && Equals((Coordinate)obj);

    public override string ToString() => $"{Column}{Row}";

    public static bool operator ==(Coordinate left, Coordinate right) => left.Equals(right);

    public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);
}
