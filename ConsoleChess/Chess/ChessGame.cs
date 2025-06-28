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
        public Piece EnPassantVulnerable { get; private set; }

        public ChessGame()
        {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            EnPassantVulnerable = null;
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

            // #SpecialMove Short Castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestination = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseMovesQuantity();
                Board.SetPiece(rook, rookDestination);
            }

            // #SpecialMove Long Castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestination = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseMovesQuantity();
                Board.SetPiece(rook, rookDestination);
            }

            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position pawnPositon;
                    if (piece.Color == Color.White)
                    {
                        pawnPositon = new Position(destination.Row + 1, destination.Column);
                    }
                    else
                    {
                        pawnPositon = new Position(destination.Row - 1, destination.Column);
                    }

                    capturedPiece = Board.RemovePiece(pawnPositon);
                    _capturedPieces.Add(capturedPiece);
                }
            }

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

            // #SpecialMove Short Castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestination = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestination);
                rook.DecreaseMovesQuantity();
                Board.SetPiece(rook, rookOrigin);
            }

            // #SpecialMove Long Castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestination = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestination);
                rook.DecreaseMovesQuantity();
                Board.SetPiece(rook, rookOrigin);
            }

            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == EnPassantVulnerable)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnPosition;
                    if (pawn.Color == Color.White)
                    {
                        pawnPosition = new Position(3, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destination.Column);
                    }

                    Board.SetPiece(pawn, pawnPosition);
                }
            }
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

            Piece piece = Board.Piece(destination);

            // #SpecialMove En Passant
            if (piece is Pawn && (destination.Row == origin.Row - 2 || destination.Row == origin.Row + 2))
            {
                EnPassantVulnerable = piece;
            }
            else
            {
                EnPassantVulnerable = null;
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
            SetNewPiece('a', 1, new Rook(Color.White, Board));
            SetNewPiece('b', 1, new Knight(Color.White, Board));
            SetNewPiece('c', 1, new Bishop(Color.White, Board));
            SetNewPiece('d', 1, new Queen(Color.White, Board));
            SetNewPiece('e', 1, new King(Color.White, Board, this));
            SetNewPiece('f', 1, new Bishop(Color.White, Board));
            SetNewPiece('g', 1, new Knight(Color.White, Board));
            SetNewPiece('h', 1, new Rook(Color.White, Board));
            SetNewPiece('a', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('b', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('c', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('d', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('e', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('f', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('g', 2, new Pawn(Color.White, Board, this));
            SetNewPiece('h', 2, new Pawn(Color.White, Board, this));

            SetNewPiece('a', 8, new Rook(Color.Black, Board));
            SetNewPiece('b', 8, new Knight(Color.Black, Board));
            SetNewPiece('c', 8, new Bishop(Color.Black, Board));
            SetNewPiece('d', 8, new Queen(Color.Black, Board));
            SetNewPiece('e', 8, new King(Color.Black, Board, this));
            SetNewPiece('f', 8, new Bishop(Color.Black, Board));
            SetNewPiece('g', 8, new Knight(Color.Black, Board));
            SetNewPiece('h', 8, new Rook(Color.Black, Board));
            SetNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            SetNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }
    }
}
