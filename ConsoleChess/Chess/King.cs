using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class King : Piece
    {
        private ChessGame _game;

        public King(Color color, ChessBoard board, ChessGame game) : base(color, board) 
        {
            _game = game;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece == null || piece.Color != Color;
        }

        private bool RookTestForCastling(Position pos)
        {
            Piece piece = Board.Piece(pos);

            return piece != null && piece is Rook && piece.Color == Color && piece.MovesQuantity == 0;
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

            // #SpecialMove Castling
            if (MovesQuantity == 0 && !_game.Check)
            {
                // #SpecialMove Short Castling
                Position rook1Position = new Position(Position.Row, Position.Column + 3);
                if (RookTestForCastling(rook1Position))
                {
                    Position pos1 = new Position(Position.Row, Position.Column + 1);
                    Position pos2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.Piece(pos1) == null && Board.Piece(pos2) == null)
                    {
                        possibleMoves[Position.Row, Position.Column + 2] = true;
                    }
                }

                // #SpecialMove Long Castling
                Position rook2Position = new Position(Position.Row, Position.Column - 4);
                if (RookTestForCastling(rook2Position))
                {
                    Position pos1 = new Position(Position.Row, Position.Column - 1);
                    Position pos2 = new Position(Position.Row, Position.Column - 2);
                    Position pos3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.Piece(pos1) == null && Board.Piece(pos2) == null && Board.Piece(pos3) == null)
                    {
                        possibleMoves[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return possibleMoves;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
