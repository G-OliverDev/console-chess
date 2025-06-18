using ConsoleChess.Board;
using ConsoleChess.Chess;

namespace ConsoleChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessPosition position = new ChessPosition('c', 7);

            Console.WriteLine(position);

            Console.WriteLine(position.ToPosition());
        }
    }
}
