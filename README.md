# General information

This is a simple implementation of battleship game coded as part of a recruitment assignement. It consists of three projects:

-   `Domain` has all of the business objects involved in the game enforcing all of it's rules
-   `Console` is a simple console app which presents a sample game. It presents a series of simple prompts which ask user to provide positions for their ships. It is left as simple as possible, because it should just allow for a simple interaction with the game (no validation of user input included).
-   `Tests` which has a set of unit tests to cover most important business rules

# Development context

The task was to code the game as I would approach any other typical work task. It means this code is not by any chance perfect - it has known limitations, not every edge case is covered and the tests have not 100% percent coverage, which corresponds with how the software is being developed in a real business environment. Knowingly I decided not to dwell into some details and focus on main business requirements. There is a ton of potential for expanding provided functionality.

This code follows most basic software development principles like SOLID, DRY, KISS - where it made sens I was trying to abstract thing, hide behind interfaces or generalize in a common parent class. I realize there are many possible approaches to every coding task and this code simply depicts mine. I'm happy to discuss the code in more details during a call :-)

# Building the project and running the game

To build the solution type `dotnet build` in the top directory.

To launch the game clone the repository. Then launch terminal in the to directory and type `dotnet run --project .\battleships.Console\battleships.Console.csproj`. All the projects are targeting `net6.0`.
