using battleships.Domain.Gameplay.ShipsGeneration;

var ships = new List<Ship>
{
    new Carrier("A1", ShipOrientation.Horizontal),
    new Battleship("A10", ShipOrientation.Vertical),
    new Battleship("C4", ShipOrientation.Horizontal),
    new Cruiser("G3", ShipOrientation.Horizontal),
    new Cruiser("H8", ShipOrientation.Vertical),
    new Cruiser("D1", ShipOrientation.Vertical),
    new Destroyer("E5", ShipOrientation.Horizontal),
    new Destroyer("J1", ShipOrientation.Horizontal),
    new Destroyer("I4", ShipOrientation.Horizontal),
    new Destroyer("A7", ShipOrientation.Horizontal),
};

var game = new Game(new GameSettings(new RandomShipsGenerationStrategy())) {};
game.Prepare(ships);

do
{
    Console.WriteLine("Provide coordinates to shoot");
    var coordinate = Console.ReadLine();
    var result = game.MakeMove(coordinate!);
    Console.WriteLine(result);
}
while (!game.IsOver);

Console.WriteLine($"Game over. Winner: {game.Winner}");