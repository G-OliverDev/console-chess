using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class Rook : Piece
    {
        public Rook(Color color, ChessBoard board) : base(color, board) { }

        public override string ToString()
        {
            return "R";
        }
    }
}
