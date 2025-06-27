using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class Knight : Piece
    {
        public Knight(Color color, ChessBoard board) : base(color, board) { }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            pos.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                possibleMoves[pos.Row, pos.Column] = true;

            return possibleMoves;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
