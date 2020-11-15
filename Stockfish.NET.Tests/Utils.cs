using System;
using System.IO;
using System.Linq;

namespace Stockfish.NET.Tests
{
    public class Utils
    {
        public static string GetStockfishDir()
        {
            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name == "Stockfish.NET");
            var location = assembly?.Location; 
            var dir =Directory.GetParent(Directory.GetParent(Directory
                .GetParent(Directory.GetParent(Directory.GetParent(location).ToString())
                    .ToString()).ToString()).ToString());
            Console.WriteLine(dir);
            var path = $@"{dir}\Stockfish.NET.Tests\Stockfish\win\stockfish_12_win_x64\stockfish_20090216_x64.exe";
            return path;
        }
    }
}