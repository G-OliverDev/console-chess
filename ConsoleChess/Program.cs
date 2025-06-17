using ConsoleChess.Board;

namespace ConsoleChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position p;

            p = new Position(3, 4);

            Console.WriteLine("Position: " + p);
        }
    }
}
