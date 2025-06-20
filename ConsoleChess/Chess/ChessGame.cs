using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class ChessGame
    {
        public ChessBoard Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;
        public bool Check { get; private set; }

        public ChessGame()
        {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            _pieces = [];
            _capturedPieces = [];
            SetPieces();
        }

        private Piece ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreaseMovesQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(piece, destination);

            if (capturedPiece != null)
                _capturedPieces.Add(capturedPiece);

            return capturedPiece;
        }

        private void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destination);
            piece.DecreaseMovesQuantity();

            if (capturedPiece != null)
            {
                Board.SetPiece(capturedPiece, destination);
                _capturedPieces.Remove(capturedPiece);
            }

            Board.SetPiece(piece, origin);
        }

        public void PlayMove(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMove(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);

                throw new ChessBoardException("You can't put yourself in check!");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
                Check = true;
            else
                Check = false;

            if (CheckmateTest(Opponent(CurrentPlayer)))
                Finished = true;
            else
            {
                Turn++;
                ChangeCurrentPlayer();
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
                throw new ChessBoardException("There is no piece on the selected origin position!");

            if (CurrentPlayer != Board.Piece(position).Color)
                throw new ChessBoardException("The selected piece doesn't belong to you!");

            if (!Board.Piece(position).AreTherePossibleMoves())
                throw new ChessBoardException("The selected piece has no legal moves!");
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).PossibleMove(destination))
                throw new ChessBoardException("Invalid destination position!");
        }

        private void ChangeCurrentPlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = [];
            foreach (Piece piece in _capturedPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = [];
            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            aux.ExceptWith(CapturedPieces(color));

            return aux;
        }   

        private Color Opponent(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in InGamePieces(color))
            {
                if (piece is King)
                    return piece;
            }

            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);

            if (king == null)
                throw new ChessBoardException("There is no king of the color " + color + " on the board!");

            foreach (Piece piece in InGamePieces(Opponent(color)))
            {
                bool[,] possibleMoves = piece.PossibleMoves();

                if (possibleMoves[king.Position.Row, king.Position.Column])
                    return true;
            }

            return false;
        }

        private bool CheckmateTest(Color color)
        {
            if (!IsInCheck(color))
                return false;

            foreach (Piece piece in InGamePieces(color))
            {
                bool[,] possibleMoves = piece.PossibleMoves();

                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMoves[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, destination);
                            bool checkTest = IsInCheck(color);

                            UndoMove(origin, destination, capturedPiece);

                            if (!checkTest)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        private void SetNewPiece(char column, int row, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void SetPieces()
        {
            SetNewPiece('c', 1, new Rook(Color.White, Board));
            SetNewPiece('d', 1, new King(Color.White, Board));
            SetNewPiece('h', 7, new Rook(Color.White, Board));

            SetNewPiece('a', 8, new King(Color.Black, Board));
            SetNewPiece('b', 8, new Rook(Color.Black, Board));
        }
    }
}
