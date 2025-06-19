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

        public ChessGame()
        {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            _pieces = [];
            _capturedPieces = [];
            SetPieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreaseMovesQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(piece, destination);

            if (capturedPiece != null)
                _capturedPieces.Add(capturedPiece);
        }

        public void PlayMove(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            Turn++;
            ChangeCurrentPlayer();
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
            if (!Board.Piece(origin).CanMoveTo(destination))
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

        private void SetNewPiece(char column, int row, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void SetPieces()
        {
            SetNewPiece('c', 1, new Rook(Color.White, Board));
            SetNewPiece('c', 2, new Rook(Color.White, Board));
            SetNewPiece('d', 2, new Rook(Color.White, Board));
            SetNewPiece('e', 2, new Rook(Color.White, Board));
            SetNewPiece('e', 1, new Rook(Color.White, Board));
            SetNewPiece('d', 1, new King(Color.White, Board));

            SetNewPiece('c', 7, new Rook(Color.Black, Board));
            SetNewPiece('c', 8, new Rook(Color.Black, Board));
            SetNewPiece('d', 7, new Rook(Color.Black, Board));
            SetNewPiece('e', 7, new Rook(Color.Black, Board));
            SetNewPiece('e', 8, new Rook(Color.Black, Board));
            SetNewPiece('d', 8, new King(Color.Black, Board));
        }
    }
}
