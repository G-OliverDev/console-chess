using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class ChessGame
    {
        public ChessBoard Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessGame()
        {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            SetPieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreaseMovesQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(piece, destination);
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

        private void SetPieces()
        {
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.SetPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.SetPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}
