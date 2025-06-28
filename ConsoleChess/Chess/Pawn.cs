using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class Pawn : Piece
    {
        private ChessGame _game;

        public Pawn(Color color, ChessBoard board, ChessGame game) : base(color, board)
        {
            _game = game;
        }

        private bool IsThereEnemy(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece != null && piece.Color != Color;
        }

        private bool IsFree(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos))
                    possibleMoves[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && MovesQuantity == 0)
                    possibleMoves[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    possibleMoves[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    possibleMoves[pos.Row, pos.Column] = true;

                // #SpecialMove En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && IsThereEnemy(left) && Board.Piece(left) == _game.EnPassantVulnerable)
                    {
                        possibleMoves[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereEnemy(right) && Board.Piece(right) == _game.EnPassantVulnerable)
                    {
                        possibleMoves[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos))
                    possibleMoves[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && MovesQuantity == 0)
                    possibleMoves[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    possibleMoves[pos.Row, pos.Column] = true;

                pos.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    possibleMoves[pos.Row, pos.Column] = true;

                // #SpecialMove En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && IsThereEnemy(left) && Board.Piece(left) == _game.EnPassantVulnerable)
                    {
                        possibleMoves[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereEnemy(right) && Board.Piece(right) == _game.EnPassantVulnerable)
                    {
                        possibleMoves[right.Row + 1, right.Column] = true;
                    }
                }
            }

            return possibleMoves;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
