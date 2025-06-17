using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class King : Piece
    {
        public King(Color color, ChessBoard board) : base(color, board) { }

        public override string ToString()
        {
            return "K";
        }
    }
}
