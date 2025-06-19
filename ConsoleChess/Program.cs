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
                    try
                    {
                        Console.Clear();
                        Screen.PrintGame(game);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = game.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(game.Board, possibleMoves);

                        Console.Write("\nDestination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        game.ValidateDestinationPosition(origin, destination);

                        game.PlayMove(origin, destination);
                    }
                    catch (ChessBoardException exception)
                    {
                        Console.WriteLine(exception.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (ChessBoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
