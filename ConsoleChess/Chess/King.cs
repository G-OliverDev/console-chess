using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class King : Piece
    {
        public King(Color color, ChessBoard board) : base(color, board) { }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            // Above
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Northeast
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Right
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Southeast
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Below
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Southwest
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Left
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            // Northwest
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            return possibleMoves;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
