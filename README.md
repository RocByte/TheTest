# Battleships game
.Net 7 console application written using C# 11.0.

Allows a single human player to play one target practice match (one-sided game of Battleships against ships placed by the computer).
The program creates a 10x10 grid, places 3 ships on it at random with the following sizes:
- 1x (5 squares)
- 2x (4 squares)
Ships can be placed next to each other but can't overlap.

The player enters coordinates to specify a cell to target. 
Exmaple: “A5”, where “A” is the column and “5” is the row.

Shots result in hits, misses or sinks that are shown in console.

The game ends when all ships are sunk or player types "esc";

## Getting Started
### Prerequisites
Install .NET Runtime 7.0.1:
https://dotnet.microsoft.com/en-us/download/dotnet/7.0

Developed using JetBrains Rider 2022.3.1 (should also work in Visual Studio 2022)

### How to run
1. Clone repo, build and run BattleshipsGameConsole.exe
2. Download Game\BattleshipsGameConsole.zip, unpack, run BattleshipsGameConsole.exe.