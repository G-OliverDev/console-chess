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
                ChessBoard board = new ChessBoard(8, 8);

                board.SetPiece(new Rook(Color.Black, board), new Position(0, 0));
                board.SetPiece(new Rook(Color.Black, board), new Position(1, 9));
                board.SetPiece(new King(Color.Black, board), new Position(0, 2));

                Screen.PrintBoard(board);
            }
            catch (ChessBoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
