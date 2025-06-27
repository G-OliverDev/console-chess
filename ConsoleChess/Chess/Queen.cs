using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class Queen : Piece
    {
        public Queen(Color color, ChessBoard board) : base(color, board) { }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            // Left
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row, pos.Column - 1);
            }

            // Right
            pos.SetValues(Position.Row, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row, pos.Column + 1);
            }

            // Above
            pos.SetValues(Position.Row - 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row - 1, pos.Column);
            }

            // Below
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row + 1, pos.Column);
            }

            // Northeast
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row - 1, pos.Column + 1);
            }

            // Southeast
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row + 1, pos.Column + 1);
            }

            // Southwest
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row + 1, pos.Column - 1);
            }

            // Northwest
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                possibleMoves[pos.Row, pos.Column] = true;

                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;

                pos.SetValues(pos.Row - 1, pos.Column - 1);
            }

            return possibleMoves;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
