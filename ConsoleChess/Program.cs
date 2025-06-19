using ConsoleChess.Board;
using ConsoleChess.Chess;

namespace ConsoleChess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessGame game = new ChessGame();

                while (!game.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(game.Board);

                    Console.Write("\nOrigin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possibleMoves = game.Board.Piece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(game.Board, possibleMoves);

                    Console.Write("\nDestination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    game.ExecuteMove(origin, destination);
                }
            }
            catch (ChessBoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
