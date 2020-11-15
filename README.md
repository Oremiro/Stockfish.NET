# Stockfish.NET

Interloop with Stockfish chess engine in c# language on .net platform

OS supported:
- Windows
- Linux
- MacOS

# Install

Package Manager:
```sh
PM> Install-Package Stockfish.NET -Version 1.0.7
```

.NET CLI:
```sh
> dotnet add package Stockfish.NET --version 1.0.7
```

Package reference
```sh
<PackageReference Include="Stockfish.NET" Version="1.0.7" />
```

# Usage examples
### Create stockfish class

You shold download stockfish engine in your OS.  Current version, which tested and used (Stockfish 12)
```c#
static void Main(string[] args)
{
    IStockfish stockfish = new Stockfish.NET.Stockfish(@"path\to\stockfish\file");
}
```

### Set position
Input:
```c#
stockfish.SetPosition("e2e4", "e7e6");
```
### Get best move
Input:
```c#
var bestMove = stockfish.GetBestMove();
```
Output:
```c#
d2d4
```
### Get evaluation
Input:
```c#
var bestMove = stockfish.GetEvaluation();
```
Output:
```c#
cp
```
# License

MIT License. Please see License file (LICENSE) for more information.
