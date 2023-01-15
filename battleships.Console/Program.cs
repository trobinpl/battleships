using battleships.Domain.Board;
using battleships.Domain.Gameplay.ShipsGeneration;

List<Ship> ships = new List<Ship>();

Console.WriteLine("Please provide ships positions by specyfing first ship coordinate and it's orientation (for example A4 H)");
Console.WriteLine("Allowed ship orientations: H for horizontal or V for vertical");
Console.WriteLine("Default ships requirements are:");
Console.WriteLine("- 1 carrier (5 blocks)");
Console.WriteLine("- 2 battleships (4 blocks)");
Console.WriteLine("- 3 cruisers (3 blocks)");
Console.WriteLine("- 4 destroyers (2 blocks)");


Console.WriteLine("Carrier#1 position");
(Coordinate carrierPosition, ShipOrientation carrierOrientation) = GetFromInput(Console.ReadLine()!);
ships.Add(new Carrier(carrierPosition, carrierOrientation));

for (int i = 1; i <= 2; i++)
{
    Console.WriteLine($"Battleship#{i} position");
    (Coordinate position, ShipOrientation orientation) = GetFromInput(Console.ReadLine()!);
    ships.Add(new Battleship(position, orientation));
}

for (int i = 1; i <= 3; i++)
{
    Console.WriteLine($"Cruiser#{i} position");
    (Coordinate position, ShipOrientation orientation) = GetFromInput(Console.ReadLine()!);
    ships.Add(new Cruiser(position, orientation));
}

for (int i = 1; i <= 4; i++)
{
    Console.WriteLine($"Destroyer#{i} position");
    (Coordinate position, ShipOrientation orientation) = GetFromInput(Console.ReadLine()!);
    ships.Add(new Destroyer(position, orientation));
}

var game = new Game(new GameSettings(new RandomShipsGenerationStrategy()));

game.Prepare(ships);

Console.WriteLine("Ships ready. Game begins!");

do
{
    Console.WriteLine("Provide coordinates to shoot");
    var coordinate = Console.ReadLine();
    (MoveResult playerMoveResult, MoveResult? computerMoveResult) = game.MakeMove(coordinate!);
    Console.WriteLine($"[Player] {playerMoveResult}");
    if (computerMoveResult is not null)
    {
        Console.WriteLine($"[Computer] {computerMoveResult}");
    }
}
while (!game.IsOver);

Console.WriteLine($"Game over. Winner: {game.Winner}");

static (Coordinate position, ShipOrientation orientation) GetFromInput(string input)
{
    var splitInput = input.Split(" ");
    return ((Coordinate)splitInput[0], splitInput[1].ToUpperInvariant() == "H" ? ShipOrientation.Horizontal : ShipOrientation.Vertical);
}