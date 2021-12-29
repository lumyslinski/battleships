# About
Simple version of the game Battleships which allows a single human player to play a one-sided game against ships placed by the computer.

The program create a 10x10 grid and place several ships on the grid at random with the following sizes:

· 1x Battleship (5 squares)

· 2x Destroyers (4 squares)

The player enters coordinates of the form "A5", where "A" is the column and "5" is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

# Project
You need to install [.NET Core 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
### Running
You can run this console app from command line where Battleships is the directory of csproj of console app. Example:
```
repo\battleships\Battleships> dotnet run
```
